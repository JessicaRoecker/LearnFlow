document.addEventListener('DOMContentLoaded', function () {
    const fileInput = document.getElementById('file-upload');
    const fileInfo = document.getElementById('file-info');
    const uploadButton = document.getElementById('upload-button');

    function displayFileName(file) {
        if (file) {
            fileInfo.innerHTML = `
            <i class="fas fa-file-pdf" style="color: red;"></i> 
            <strong>Arquivo selecionado:</strong> ${file.name}`;
            fileInfo.style.color = "#333"; 
        } else {
            fileInfo.innerHTML = `
            <strong>Nenhum arquivo selecionado.</strong>`;
            fileInfo.style.color = "#FF0000"; 
        }
    }

    // Evento de seleção de arquivo
    fileInput.addEventListener('change', function () {
        const file = fileInput.files[0];
        displayFileName(file);
    });

    // Evento de clique no botão de envio
    uploadButton.addEventListener('click', function () {
        const file = fileInput.files[0];
        if (file) {
            alert(`Arquivo "${file.name}" pronto para ser enviado!`);
            // Aqui você pode adicionar lógica para enviar o arquivo para o servidor
        } else {
            fileInfo.textContent = "Por favor, selecione um arquivo PDF primeiro.";
        }
    });
});
