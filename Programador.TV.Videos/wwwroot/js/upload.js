var videoPreview = document.getElementById('videoPreview');
var inputFile = document.getElementById('videoFile');

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
        })
});

inputFile.addEventListener('change', function () {
    var file = this.files[0];

    if (file && file.type.startsWith('video/')) {
        videoPreview.src = URL.createObjectURL(file);
        videoPreview.style.display = 'block'; 
    } else {
        videoPreview.style.display = 'none';
    }
});

