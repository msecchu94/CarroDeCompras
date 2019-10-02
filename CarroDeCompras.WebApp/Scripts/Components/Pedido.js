
$('.btnAgregarPedido').on('click', function (e) {
    $('#form-pedido').submit();
});


$('#form-pedido').on('submit', function (e) {

    e.preventDefault();
    console.log(this);
    console.log('Submit form carrito');

    const Observaciones = $('.Observaciones').val();

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
                data: { Observaciones: Observaciones }

            })
                .done(function (data) {
                    console.log(data);

                    if (data.Success) {

                        location.reload();

                        Swal.fire({
                            type: 'success',
                            title: 'Pedido Agregado con Exito',
                            showConfirmButton: false,
                            timer: 1500
                        });
                    }
                })
                .fail(function (data) {

                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: 'Error al agregar el Pedido',
                    });
                    console.log(data);

                });

        }
    });

});

