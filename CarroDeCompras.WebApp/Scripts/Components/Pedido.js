
$('.btnAgregarPedido').on('click', function (e) {
    $('#form-pedido').submit();
});


$('#form-pedido').on('submit', function (e) {

    e.preventDefault();
    console.log(this);
    console.log('Submit form carrito');


    let DetallesPedido = [];
    let pedidoModel = {
        Observaciones: $('.Observaciones').val(),
        DetallesPedido
    };

    $('#tablacarro tbody tr').each(function (index, el) {

        DetallesPedido = {
            Cantidad: $(el).data().cantidad,
            Producto: {
                Codigo: $(el).data('codigo-producto')
            }
        };
        pedidoModel.DetallesPedido.push(DetallesPedido);

    });
    console.log(pedidoModel);

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

                data: {
                    Observaciones: Observaciones,

                }

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

$('.btnVerPedido').on('click', function () {

    console.log(this);

    //creamos var con la data pasada por el btnPedido 
    const pedidoObservaciones = this.dataset.pedidoObservaciones;
    const pedidoTotal = this.dataset.pedidoTotal;
    const pedidoDetalles = this.dataset.pedido;
    var items = [];
    items = this.this.dataset.detalles;
    Console.log(items);


    // Llenar los datos del modal
    const $modal = $('#myModalPedido');
    $modal.find('.modal-observaciones').html(pedidoObservaciones);
    $modal.find('.descripcion-detalles').html(pedidoDetalles);
    $modal.find('.pedidoTotal').val(pedidoTotal);
    $modal.find('.pedidoTotal').val(pedidoTotal);


    // Abrir
    $modal.modal('show');
});




