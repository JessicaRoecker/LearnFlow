document.addEventListener('DOMContentLoaded', function () {
    let tentativasErradas = 0;
    let tempoBloqueio = null;

    const loginDataContract = {
        Email: '',
        SenhaHash: ''
    };

    const loginForm = document.getElementById("loginForm");
    if (loginForm) {
        loginForm.addEventListener("submit", async (event) => {
            event.preventDefault();

            if (tempoBloqueio && new Date().getTime() < tempoBloqueio) {
                const tempoRestante = Math.ceil((tempoBloqueio - new Date().getTime()) / 1000);
                const passwordError = document.getElementById("passwordError");
                passwordError.textContent = `Você está bloqueado por ${tempoRestante} segundos.`;
                passwordError.style.display = "block";
                return;
            }

            const emailInput = document.getElementById("email");
            const passwordInput = document.getElementById("password");
            const emailError = document.getElementById("emailError");
            const passwordError = document.getElementById("passwordError");

            // Resetando mensagens de erro
            emailError.style.display = "none";
            passwordError.style.display = "none";

            let hasError = false;

            // Validação de e-mail
            if (!emailInput.value || !validarEmail(emailInput.value)) {
                emailError.textContent = !emailInput.value
                    ? "Por favor, insira seu e-mail."
                    : "O e-mail fornecido é inválido.";
                emailError.style.display = "block";
                hasError = true;
            }

            // Validação de senha
            if (!passwordInput.value) {
                passwordError.textContent = "Por favor, insira sua senha.";
                passwordError.style.display = "block";
                hasError = true;
            }

            if (hasError) {
                return;
            }

            loginDataContract.Email = emailInput.value;
            loginDataContract.SenhaHash = passwordInput.value;

            await realizarLogin();
        });
    }

    async function realizarLogin() {
        const emailError = document.getElementById("emailError");
        const passwordError = document.getElementById("passwordError");

        try {
            const loginBtn = document.getElementById("loginBtn");
            loginBtn.disabled = true;

            const response = await fetch("/Autenticacao", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(loginDataContract),
            });

            const responseData = await response.json();

            if (response.ok) {
                alert("Login realizado com sucesso!");
                window.location.href = "/Index";
                tentativasErradas = 0;
            } else {
                if (responseData.message === "Senha inválida") {
                    tentativasErradas++;

                    if (tentativasErradas >= 3) {
                        tempoBloqueio = new Date().getTime() + 5 * 60 * 1000;
                        passwordError.textContent = "Você errou a senha 3 vezes. Sua conta está bloqueada por 5 minutos.";
                        passwordError.style.display = "block";
                    } else {
                        passwordError.textContent = "Senha inválida. Tente novamente.";
                        passwordError.style.display = "block";
                    }
                } else if (responseData.message === "Email não cadastrado") {
                    emailError.textContent = "Email não cadastrado.";
                    emailError.style.display = "block";
                } else {
                    passwordError.textContent = responseData.message || "Erro desconhecido. Por favor, tente novamente.";
                    passwordError.style.display = "block";
                }
            }
        } catch (error) {
            passwordError.textContent = "Erro no servidor. Por favor, tente novamente mais tarde.";
            passwordError.style.display = "block";
        } finally {
            const loginBtn = document.getElementById("loginBtn");
            loginBtn.disabled = false;
        }
    }

    function validarEmail(email) {
        const regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
        return regex.test(email);
    }
});
