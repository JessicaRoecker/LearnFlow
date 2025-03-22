document.addEventListener('DOMContentLoaded', function () {
    const loginDataContract = {
        Email: '',
        SenhaHash: '',
        Nome: ''
    };

    const registerForm = document.getElementById("registerForm");
    if (registerForm) {
        registerForm.addEventListener("submit", async (event) => {
            event.preventDefault();

            const emailInput = document.getElementById("email");
            const nomeInput = document.getElementById("name");
            const passwordInput = document.getElementById("password");
            const confirmPasswordInput = document.getElementById("confirmPassword");
            const passwordError = document.getElementById("passwordError");
            const emailError = document.getElementById("emailError");
            const nomeError = document.getElementById("nomeError");

            // Resetando mensagens de erro
            emailError.style.display = "none";
            passwordError.style.display = "none";
            nomeError.style.display = "none";

            let hasError = false;

            if (!nomeInput.value) {
                nomeError.textContent = "Insira o seu nome";
                nomeError.style.display = "block";
                hasError = true;
            }

            if (!emailInput.value || !validarEmail(emailInput.value)) {
                emailError.textContent = !emailInput.value
                    ? "Por favor, insira seu e-mail."
                    : "O e-mail fornecido é inválido.";
                emailError.style.display = "block";
                hasError = true;
            }

            if (!passwordInput.value) {
                passwordError.textContent = "Por favor, insira sua senha.";
                passwordError.style.display = "block";
                hasError = true;
            }

            if (passwordInput.value !== confirmPasswordInput.value) {
                passwordError.textContent = "As senhas devem ser iguais.";
                passwordError.style.display = "block";
                hasError = true;
            }

            if (!validarSenha(passwordInput.value)) {
                passwordError.textContent = "A senha deve conter: Pelo menos 8 caracteres - Letra MAIUSCULA e minuscula - Ter pelo menos um caracter especial"
                passwordError.style.display = "block";
            }

            if (hasError) {
                return;
            }

            loginDataContract.Email = emailInput.value;
            loginDataContract.SenhaHash = passwordInput.value;
            loginDataContract.Nome = nomeInput.value;
            await realizarCadastro();

        });
    }

    async function realizarCadastro() {
        try {
            const loginBtn = document.getElementById("registerBtn");
            loginBtn.disabled = true;

            const response = await fetch("/Cadastro", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(loginDataContract),
            });

            console.log("Status:", response.status);
            console.log("Response OK:", response.ok);

            // Verificar cabeçalho Content-Type
            console.log("Content-Type:", response.headers.get("Content-Type"));

            let responseData;
            try {
                responseData = await response.json();
                console.log("Response Data:", responseData);
            } catch (jsonError) {
                console.error("Erro ao interpretar JSON:", jsonError);
                const responseText = await response.text();
                console.log("Resposta em texto:", responseText);
                throw new Error("Erro ao processar a resposta do servidor.");
            }

            if (response.ok) {
                console.log("Entrou no bloco do if. Status OK.");
                alert("Cadastro realizado com sucesso!");

                window.location.href = "/Index";
            } else if (responseData.message === "Já existe uma conta registrada com o email fornecido") {
                emailError.textContent = "Já existe uma conta registrada com o email fornecido";
                emailError.style.display = "block";
            } else {
                alert(responseData.message || "Erro desconhecido. Tente novamente.");
            }
        } catch (error) {
            console.error("Erro no cadastro:", error);
            alert("Erro no servidor. Por favor, tente novamente mais tarde.");
        } finally {
            const loginBtn = document.getElementById("registerBtn");
            loginBtn.disabled = false;
        }
    }

    function validarEmail(email) {
        const regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
        return regex.test(email);
    }

    function validarSenha(senha) {
        // Expressão regular para validar os requisitos:
        const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*(),.?":{}|<>])[A-Za-z\d!@#$%^&*(),.?":{}|<>]{8,}$/;

        // Testa se a senha atende aos critérios
        if (regex.test(senha)) {
            return true; // Senha válida
        } else {
            return false; // Senha inválida
        }
    }

});
