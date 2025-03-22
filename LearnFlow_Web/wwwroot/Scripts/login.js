let tentativasErradas = 0;
let tempoBloqueio = null;

// Declarar loginDataContract apenas uma vez
const loginDataContract = {
    Email: '',
    SenhaHash: ''
};

document.getElementById("loginForm").addEventListener("submit", async (event) => {
    event.preventDefault();

    // Verifique se o bloqueio de 5 minutos está ativo
    if (tempoBloqueio && new Date().getTime() < tempoBloqueio) {
        const tempoRestante = Math.ceil((tempoBloqueio - new Date().getTime()) / 1000);
        alert(`Você está bloqueado por ${tempoRestante} segundos. Tente novamente depois.`);
        return;
    }

    loginDataContract.Email = document.getElementById("email").value;
    loginDataContract.SenhaHash = document.getElementById("password").value;

    // Verifique se todos os campos estão preenchidos
    if (!loginDataContract.Email || !loginDataContract.SenhaHash) {
        alert("Preencha todos os campos!");
        return;
    }

    // Validação de email
    if (!validarEmail(loginDataContract.Email)) {
        alert("O e-mail fornecido é inválido. Por favor, insira um e-mail válido.");
        return;
    }

    try {
        const response = await fetch("Home/Login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(loginDataContract),
        });

        const responseData = await response.json();

        if (response.ok) {
            alert("Login realizado com sucesso!");
            tentativasErradas = 0; // Resetar tentativas após sucesso
        } else {
            if (responseData.message === "Senha inválida") {
                tentativasErradas++;

                if (tentativasErradas >= 3) {
                    tempoBloqueio = new Date().getTime() + 5 * 60 * 1000; // 5 minutos de bloqueio
                    alert("Você errou a senha 3 vezes. Sua conta está bloqueada por 5 minutos.");
                } else {
                    alert(`Senha inválida. Você tem ${3 - tentativasErradas} tentativas restantes.`);
                }
            } else {
                alert(responseData.message || "Erro ao realizar login.");
            }
        }
    } catch (error) {
        alert("Erro no servidor. Por favor, tente novamente mais tarde.");
    }
});

// Função para validar formato de email
function validarEmail(email) {
    const regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    return regex.test(email);
}

function validarSenha(senha) {
    const regex = /^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    return regex.test(senha);
}
