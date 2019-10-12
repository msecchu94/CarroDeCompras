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

    //e.preventDefault();
    console.log(this);
    const numeroPedido = this.dataset.pedido;
    console.log(numeroPedido);

    //$("#parcial").load("Pedido/_ModelPedido")

    $.ajax({
        url: "Pedido/_ModalPedido",
        type: "POST",
        data: {numeroPedido: numeroPedido }
    })
    .done(function(data) {
        $("#modalbodydiv").html(data);
        const $modal = $('#myModal');
        $modal.modal('show');
  
    });
               
})