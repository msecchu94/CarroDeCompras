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

    var tr = $(this).closest('tr');
    const codigo = tr.data('codigo-producto');
    console.log(codigo);

    Swal.fire({
        title: 'Estas Seguro?',
        text: "Una vez confirmado, no podrás revertir !",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar!'
    }).then((result) => {
        if (result.value) {

            $.ajax({
                method: 'POST',
            url: 'Carro/EliminarItem',
            data: {
                codigo: codigo
            }
        }).done(function (data) {
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
                        text: 'Error al Eliminar al Producto'
                                            
                    });
                    console.log(data);
                });
}
});
});

// modificar cantidad en carro
$('.cantidad').on('change', function () {


    var cantidad = $(this).val();
    cantidad = parseInt(cantidad);


    var $tr = $(this).closest('tr');
    var precioUnitarioStr = $tr.data('precio-unitario').replace(',', '.');
    var precioUnitario = parseFloat(precioUnitarioStr); // numérico
    var subtotal = cantidad * precioUnitario;
    $tr.data('subtotal', subtotal);

    var intl = new Intl.NumberFormat('es-AR', { maximumFractionDigits: 2, minimumFractionDigits: 2, useGrouping: true });
    subtotal = intl.format(subtotal);
    $tr.find('.subtotal').html('$ ' + subtotal);

    actualizarTotal();

    $tr.data('cantidad', cantidad);

});

function actualizarTotal() {
    let total = 0;
    console.log('Actualizando total...');
    $('#tablacarro tbody tr').each(function (index, el) {
        let subtotal = $(el).data().subtotal;

        if (typeof (subtotal) === 'string') {
            subtotal = parseFloat(subtotal.replace(',', '.'));
        }

        total += subtotal;
        var intl = new Intl.NumberFormat('es-AR', { maximumFractionDigits: 2, minimumFractionDigits: 2, useGrouping: true });

        console.log(subtotal);

        $('.total').html('$ ' + intl.format(total));
    });

}




