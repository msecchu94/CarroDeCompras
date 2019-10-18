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

$('#form-carro').on('submit', function (e) {

    e.preventDefault();
    console.log(this);

    const cantidad = $(".Cantidad").val();
    console.log(cantidad);
    const codigoProducto = $(".codigoProducto").val();
    console.log(codigoProducto);

    $.ajax({
        method: 'POST',
        url: 'Carro/Agregar',
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
                    title: 'Producto Agreagdo con Exito',
                    showConfirmButton: true,
                    timer: 1500
                });
            }
        })
        .fail(function (data) {
            Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Error al agregar al Producto',
            });
            console.log(data);
        });
});

$('.eliminar').on('click', function (e) {
    e.preventDefault();

    console.log(this);

    var tr = $(this).closest('tr');
    const codigoProducto = tr.data('codigo-producto');
    console.log(codigoProducto);

    Swal.fire({
        title:'Estas Seguro?',
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
        codigoProducto: codigoProducto
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




