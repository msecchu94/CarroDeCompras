$('#crear').on('click', function (e) {
    $('#form-crear').submit();
});

$('#form-crear').on('submit', function (e) {

    e.preventDefault();
    console.log(this);
    var formData = new FormData(this);

    Swal.fire({
        title: 'Estas seguro de Agregar?',

        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'

    }).then((result) => {

        if (result.value) {

            $.ajax({
                method: 'POST',
                url: this.attributes.action.value,
                data: formData,
                contentType: false,
                cache: false,
                processData: false

            })
                .done(function (data) {
                    console.log(data);

                    if (data.Success) {

                        Swal.fire({
                            type: 'success',
                            title: 'Producto Agregado con Exito',
                            showConfirmButton: false,
                            timer: 3000
                        });
                        window.location.href = '/Producto/Index/';
                    }

                })
                .fail(function (data) {

                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: 'Error al agregar el Producto'
                    });
                    console.log(data);

                });

        }
    });

});

$('#editar').on('click', function (e) {
    $('.form-editar').submit();
});

$('.form-editar').on('submit', function (e) {

    e.preventDefault();
    console.log(this);

    var formData = new FormData(this);

    Swal.fire({
        title: 'Estas seguro de Editar?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Editar!'

    }).then((result) => {
        if (result.value) {

            $.ajax({
                method: 'POST',
                url: this.attributes.action.value,
                data: formData,
                contentType: false,
                cache: false,
                processData: false

            })
                .done(function (data) {

                    console.log(data);
                    if (data.Success) {

                        Swal.fire({
                            type: 'success',
                            title: 'Producto Editado con Exito',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        window.location.href = '/Producto/Index/';
                    }
                })
                .fail(function (data) {

                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: 'Error al editar el Producto'
                    });
                    console.log(data);
                });
        }
    });

});