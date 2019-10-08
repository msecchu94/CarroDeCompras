﻿
$('.btnCarro').on('click', function () {

    console.log(this);

    //creamos var con la data pasada por el btnPedido 
    const codigoProducto = this.dataset.productoCodigo;
    const nombreProducto = this.dataset.productoNombre;
    const descripcionProducto = this.dataset.productoDescripcion;

    // Llenar los datos del modal
    const $modal = $('#myModal');
    $modal.find('.modal-title').html(codigoProducto + "-" + nombreProducto);
    $modal.find('.descripcion-body').html(descripcionProducto);
    $modal.find('.codigoProducto').val(codigoProducto);


    // Abrir
    $modal.modal('show');
});

$('.btnAgregarCarro').on('click', function (e) {
    $('#form-carro').submit();
});

//Agregar Carrito

$('#form-carro').on('submit', function (e) {

    e.preventDefault();
    console.log(this);

    const cantidad = $(".Cantidad").val();
    const codigoProducto = $(".codigoProducto").val();

    $.ajax({
        method: 'POST',
        url: 'Carro/Index',
        data: {
            cantidad: cantidad,
            codigoProducto: codigoProducto
        }
    })
        .done(function (data) {
            console.log(data);

            if (data.Success) {
                $('#myModal').modal('hide');

                Swal.fire({
                    type: 'success',
                    title: 'Pedido Enviado con Exito',
                    showConfirmButton: true,
                    timer: 1500
                });

            }

        })
        .fail(function (data) {

            Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Error al agregar al Pedido',
                footer: '<a href>Why do I have this issue?</a>'
            });
            console.log(data);

        });
});

//eliminar item carro

$('.eliminar').on('click', function (e) {
    e.preventDefault();

    console.log(this);

    const codigo = this.dataset.id;
    var tr = $(this).closest('tr');
    console.log(codigo);

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
    url: 'Carro/EliminarItem',
    data: {
        codigo: codigo
    }
})

                .done(function (data) {
                    console.log(data);

                    if (data.Success) {
                        tr.remove();
                        location.reload();

                        Swal.fire({
                            type: 'success',
                            title: 'Producto Eliminado con Exito',
                            showConfirmButton: true,
                            timer: 1500
                        });
                    }
                })
                .fail(function (data) {

                    Swal.fire({
                        type: 'error',
                        title: 'Oops...',
                        text: 'Error al Eliminar al Producto',
                        footer: '<a href>Why do I have this issue?</a>'
                    });
                    console.log(data);

                });
}
});
});

$('.cantidad').on('change',function(){
  
     
    const cantidad=$(this).val();
    const precioUnitario=$('.precioUnitario').val();
    const subtotal =$(".subtotal").val();
    const total = $('.total').val();

    console.log(precioUnitario);



});




