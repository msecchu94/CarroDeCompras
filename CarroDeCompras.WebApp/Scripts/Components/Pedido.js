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
                url: 'Pedido/CargarPedido',

                data: {
                    pedidoModel: pedidoModel
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

    //var $tr = $(this).closest('tr');
    //var numeroPedido = $tr.data('numero-pedido');
    //var detallePedido = $tr.data('detalle-pedido');
    //$tr.find(numeroPedido);
    //console.log(detallePedido);


    ////creamos var con la data pasada por el btnPedido 
    //var numeropedido = $(this).closest('tr').find('numero-pedido');
    //console.log(numeropedido);

    //const pedidoObservaciones = this.dataset.observaciones;
    //const pedidoTotal = this.dataset.total;
    //const numeroPedido = this.dataset.numeropedido;
    //var items = [];
    //items = this.dataset.detalles;

    //Console.log(pedidoObservaciones);
    //Console.log(pedidoTotal);
    //Console.log(numeroPedido);
    //Console.log(items);
    //var modalPedido = function () {
    //    var url = "/";
    //},
$('.btnVerPedido').load("Pedido/ModalPedido");

    //// Llenar los datos del modal
    const $modal = $('#myModal');
    //$modal.find('.modal-observaciones').html(pedidoObservaciones);
    //$modal.find('.descripcion-detalles').html(pedidoDetalles);
    //$modal.find('.pedidoTotal').val(pedidoTotal);
    //$modal.find('.pedidoTotal').val(pedidoTotal);


    // Abrir
    $modal.modal('show');
});




