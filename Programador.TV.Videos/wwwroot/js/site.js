document.querySelector('form').addEventListener('submit', async function (e) {
    e.preventDefault();

    const formData = new FormData(this);
    const response = await fetch(this.action, {
        method: this.method,
        body: formData
    });

    if (response.ok) {
        const json = await response.json();
        document.getElementById('jsonTextArea').value = JSON.stringify(json, null, 2);
        document.getElementById('jsonResult').style.display = 'block';
    } else {
        alert('Erro ao processar o vídeo. Verifique o arquivo enviado.');
    }
});
