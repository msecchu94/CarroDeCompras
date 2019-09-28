$('#crear').on('click', function (e) {

    e.preventDefault();
    Swal.fire({
        title: 'Seguro, desea confirmar?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, confirmar!'
    }).then((result) => {
        if (result.value) {
            $('.form-crear').submit();
    Swal.fire(
        'Confirmado!'
    );
}
});
});


$('#editar').on('click', function (e) {

    e.preventDefault();
    Swal.fire({
        title: 'Seguro, desea confirmar?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, confirmar!'
    }).then((result) => {
        if (result.value) {
            $('.form-editar').submit();
    Swal.fire(
        'Confirmado!'
    );
}
});
});
