document.getElementById('formEntrada').addEventListener('submit', function (event) {
    event.preventDefault();

    var formData = new FormData(event.target);

    fetch('/Index', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            document.getElementById('videoInfo').innerText = JSON.stringify(data);
        });
});