$('#crear').on('click', function (e) {
    $('#form-crear').submit();
});



$('#form-crear').on('submit', function (e) {

    e.preventDefault();
    console.log(this);

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
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
                data: $(this).serialize()

            })
                .done(function (data) {
                    console.log(data);

                    if (data.Success) {

                        Swal.fire({
                            type: 'success',
                            title: 'Pedido Agregado con Exito',
                            showConfirmButton: false,
                            timer: 3000
                        });
                        window.location.href = '/Producto/Index/';
                    }

                })
                .fail(function(data) {

                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: 'Error al agregar el Pedido'
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

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
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
                data: $(this).serialize()

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