function SalidaBodega(IdSlot) {
    gfProceso();


    $.ajax({
        type: "POST",
        url: "/Bodega/ConsultaSlotDescripcion",
        content: "application/json; charset=utf-8",
        dataType: "json",
        //   data: { Id_Cliente: Cliente, Id_Ruta: Ruta, Id_BaseCobro: BaseCobro, Id_TipoCarga: TipoCarga },
        data: { IdSlot: IdSlot },

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT




                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO
                detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  



                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {

                    //swal({
                    //    title: 'ORDEN DE TRANSPORTE',
                    //    text: 'Se genero Orden de TRansporte N°' + data.Descripcion + '',
                    //    type: 'success',
                    //    confirmButtonColor: '#FE6A00',
                    //    confirmButtonText: 'OK',
                    //    confirmButtonClass: 'btn btn-info'
                    //});

                    $("#LblNombreSlotSalida").text(detalle.DescripcionSlot);

                    $("#LblIDSlotSalida").text(IdSlot);


                    $('#ModalSalidaBodega').modal('show');

                    gfProceso();


                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {



                    swal({
                        title: 'ORDEN DE TRANSPORTE',
                        text: 'Error al buscar Slot en Bodega ' + data.Detalle_Error + '',
                        type: 'error',
                        confirmButtonColor: '#FE6A00',
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-info'
                    });


                    gfProceso();
                    return false;

                }

            } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT


                swal({
                    title: 'ORDEN DE TRANSPORTE',
                    text: 'Error al buscar Slot en Bodega ' + err + '',
                    type: 'error',
                    confirmButtonColor: '#FE6A00',
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-info'
                });

                gfProceso();
                return false;
            }



        },
        error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.
            alert('Error al buscar Slot en Bodega, Favor Revisar');

            gfProceso();
            return false;
        }
    });




}


function AgregarBultoSalida() {



    //AJAX APLICAR

    var Id_Paquete;
    var Id_Slot;
    var Tipo_movimiento;
    var descripcion;

    Id_Slot = $("#LblIDSlotSalida").text();
    Id_Paquete = $("#txtPistolaCodigoSalida").val();
    Tipo_movimiento = $("#CmbTipoMovimiento").val();
    descripcion = $("#txtDescripcionSalida").val();
    //url: "/Bodega/",



    gfProceso();


    $.ajax({
        type: "POST",
        url: "/Bodega/SacarPaqueteBodega",
        content: "application/json; charset=utf-8",
        dataType: "json",
        //   data: { Id_Cliente: Cliente, Id_Ruta: Ruta, Id_BaseCobro: BaseCobro, Id_TipoCarga: TipoCarga },
        data: { Id_Paquete: Id_Paquete, Id_Slot: Id_Slot, Tipo_movimiento: Tipo_movimiento, descripcion: descripcion },

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT




                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO
                detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  


                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {




                    StrTable = '<tr>'; // INICIO
                    StrTable = StrTable + '<td>' + Id_Paquete + '</td>';
                    StrTable = StrTable + '<td>' + detalle.Razon_Social + '</td>';
                    StrTable = StrTable + '<td>' + detalle.DescripcionRuta + '</td>';
                    StrTable = StrTable + '<td>' + detalle.DescripcionTipoCarga + '</td>';
                    StrTable = StrTable + '<td>' + detalle.ID_Orden_Transporte + '</td>';
                    StrTable = StrTable + '<td>' + detalle.Fecha_Ingreso + '</td>';

               //     StrTable = StrTable + '<td class="td-actions text-right"><button type="button" rel="tooltip" class="btn btn-danger" onclick="del_Bulto(this);return false;"><i class="material-icons">close</i></button></td>'; // BUTON
                    StrTable = StrTable + '</tr>'; // CIERRE



                    $('#TablaBultoSalida tr').last().after(StrTable);



                    $("#txtPistolaCodigoSalida").val();

                    $("#txtPistolaCodigoSalida").focus();





                    gfProceso();


                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {



                    swal({
                        title: 'BODEGA TOPTAINER',
                        text: 'Error al Sacar Bulto a Bodega ' + data.Detalle_Error + '',
                        type: 'error',
                        confirmButtonColor: '#FE6A00',
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-info'
                    });


                    gfProceso();
                    return false;
                }

            } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT


                swal({
                    title: 'BODEGA TOPTAINER',
                    text: 'Error al Sacar Bulto a Bodega ' + data.Detalle_Error + '',
                    type: 'error',
                    confirmButtonColor: '#FE6A00',
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-info'
                });
                gfProceso();
                return false;
            }



        },
        error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.

            swal({
                title: 'BUSCAR INFORMACIÓN BULTO',
                text: 'Error  ' + errorThrown + '',
                type: 'error',
                confirmButtonColor: '#FE6A00',
                confirmButtonText: 'OK',
                confirmButtonClass: 'btn btn-info'
            });

            gfProceso();
            return false;
        }
    });








}
 

function VerRecepcion(IdSlot)

   
{
   
    gfProceso();


   

    $.ajax({
        type: "POST",
        url: "/Bodega/ConsultaDetalleSlot",
        content: "application/json; charset=utf-8",
        dataType: "json",
        //   data: { Id_Cliente: Cliente, Id_Ruta: Ruta, Id_BaseCobro: BaseCobro, Id_TipoCarga: TipoCarga },
        data: { IdSlot: IdSlot },

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT


              

                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO
                detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  




                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {

                    $("#TablaDetalleSlot tr>td").remove();


                  

                    for (var i in detalle) {
                        StrTable = '<tr>'; // INICIO
                        StrTable = StrTable + '<td><a onclick="#">' + detalle[i].ID_Paquete + '</a></td>';
                        StrTable = StrTable + '<td><a onclick="#">' + detalle[i].ID_Orden_Transporte + '</a></td>';
                        StrTable = StrTable + '<td>' + detalle[i].Razon_Social + '</td>';
                        StrTable = StrTable + '<td>' + detalle[i].DescripcionRuta + '</td>';
                        StrTable = StrTable + '<td>' + detalle[i].DescripcionTipoCarga + '</td>';
                        StrTable = StrTable + '<td>' + detalle[i].DescripcionEmbalaje + '</td>';
                      

                        StrTable = StrTable + '<td>';


                        var detalleGuias;

                        detalleGuias = jQuery.parseJSON(JSON.stringify(detalle[i].Guias));


                        for (var j in detalleGuias) {
                            StrTable = StrTable + '<a href=/guias/showUserPDF?IDGuias=' + detalleGuias[j].ID_Guia_Cliente + ' data-toggle="tooltip" title="' + detalleGuias[j].Numero_Guia + '" target="_blank"><i class="material-icons">description</i></a>';

                        }

                        StrTable = StrTable + '</td>';


                        StrTable = StrTable + '<td>' + detalle[i].Fecha_Ingreso + '</td>';
                        StrTable = StrTable + '<td>' + detalle[i].DescripcionSucursal + '</td>';
                        StrTable = StrTable + '<td>' + detalle[i].NombreUsuario + '</td>';

                        
                        
                    //   StrTable = StrTable + '<td class="td-actions text-right"><button type="button" rel="tooltip" class="btn btn-danger" onclick="del_Bulto(this);return false;"><i class="material-icons">close</i></button></td>'; // BUTON
                        StrTable = StrTable + '</tr>'; // CIERRE

                        $('#lblTituloSlot').text(detalle[0].DescripcionSlot);


                        $('#TablaDetalleSlot tr').last().after(StrTable);
                        i++;
                    }

                   




                    $('#ModalDetalleSlot').modal('show');




                    gfProceso();


                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {



                    swal({
                        title: 'TOPTAINER BODEGA',
                        text: 'No existen Bultos en Slot de Bodega ' + data.Detalle_Error + '',
                        type: 'error',
                        confirmButtonColor: '#FE6A00',
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-info'
                    });


                    gfProceso();

                    return false;
                }

            } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT


                swal({
                    title: 'TOPTAINER BODEGA',
                    text: 'No existen Bultos en Slot de Bodega ' + data.Detalle_Error + '',
                    type: 'error',
                    confirmButtonColor: '#FE6A00',
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-info'
                });
                gfProceso();


                return false;
            }



        },
        error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.
            alert('Error de Conexión al guardar la Orden de TRanspoete, Favor Revisar');

            gfProceso();
            return false;
        }
    });



       
}
   $('#txtPistolaCodigoIngreso').keypress(function (event) {
        if(event.keyCode == 13){
            $('#Btn_AgregaBulto').click();
        }
   });


   $('#txtPistolaCodigoSalida').keypress(function (event) {
       if (event.keyCode == 13) {
           $('#Btn_AgregaBulto_Salida').click();
       }
   });
   

    $(document).keypress(function (e) {

//PARA PISTOLA !

    if (e.which == 13 || event.keyCode == 13) {

        return false;
    }


});
function AgregarBulto()

{
    
    

    //AJAX APLICAR

    var Id_Paquete;
    var Id_Slot;

    Id_Slot   = $("#LblIDSlot").text();
    Id_Paquete = $("#txtPistolaCodigoIngreso").val();

    //url: "/Bodega/ConsultaDetallePaquete",

    
    gfProceso();


     $.ajax({
        type: "POST",
        url: "/Bodega/IngresarPaqueteBodega",
        content: "application/json; charset=utf-8",
        dataType: "json",
        //   data: { Id_Cliente: Cliente, Id_Ruta: Ruta, Id_BaseCobro: BaseCobro, Id_TipoCarga: TipoCarga },
        data: { Id_Paquete: Id_Paquete, Id_Slot: Id_Slot },

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT

                


                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO
                detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  

           
                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {
                     
                


                    StrTable = '<tr>'; // INICIO
                    StrTable = StrTable + '<td>' + Id_Paquete + '</td>';
                    StrTable = StrTable + '<td>' + detalle.Razon_Social + '</td>';
                    StrTable = StrTable + '<td>' + detalle.DescripcionRuta + '</td>';
                    StrTable = StrTable + '<td>' + detalle.DescripcionTipoCarga + '</td>';
                    StrTable = StrTable + '<td>' + detalle.ID_Orden_Transporte + '</td>';
                    StrTable = StrTable + '<td>' + detalle.Fecha_Ingreso + '</td>';
                
                //    StrTable = StrTable + '<td class="td-actions text-right"><button type="button" rel="tooltip" class="btn btn-danger" onclick="del_Bulto(this);return false;"><i class="material-icons">close</i></button></td>'; // BUTON
                     StrTable = StrTable + '</tr>'; // CIERRE
                    

                
                    $('#TablaBultoIngreso tr').last().after(StrTable);




                    $("#txtPistolaCodigoIngreso").val();
                 
                    $("#txtPistolaCodigoIngreso").focus();

                    gfProceso();


                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {



                    swal({
                        title: 'BODEGA TOPTAINER',
                        text: 'Error al ingresar Bulto a Bodega ' + data.Detalle_Error + '',
                        type: 'error',
                        confirmButtonColor: '#FE6A00',
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-info'
                    });


                    gfProceso();
                    return false;               }

            } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT


                swal({
                    title: 'BODEGA TOPTAINER',
                    text: 'Error al ingresar Bulto a Bodega ' + data.Detalle_Error + '',
                    type: 'error',
                    confirmButtonColor: '#FE6A00',
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-info'
                });
                gfProceso();
                return false;
            }



        },
        error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.
           
            swal({
                title: 'BUSCAR INFORMACIÓN BULTO',
                text: 'Error  ' + errorThrown + '',
                type: 'error',
                confirmButtonColor: '#FE6A00',
                confirmButtonText: 'OK',
                confirmButtonClass: 'btn btn-info'
            });

            gfProceso();
            return false;
        }
    });





    
              

}

function foco()
{


    $("#txtPistolaCodigoIngreso").focus();

}
function del_Bulto(remtr)
 {
            while((remtr.nodeName.toLowerCase())!='tr')
            remtr = remtr.parentNode;
             remtr.parentNode.removeChild(remtr);
 }
function del_id(id)
{
    del_tr(document.getElementById(id));
}



function VerDetalleRecepcion(ID_Sucursal) {
  

   
        gfProceso();


        $.ajax({
            type: "POST",
            url: "/Bodega/ConsultaPaquetesSucursal",
            content: "application/json; charset=utf-8",
            dataType: "json",
            //   data: { Id_Cliente: Cliente, Id_Ruta: Ruta, Id_BaseCobro: BaseCobro, Id_TipoCarga: TipoCarga },
            data: { ID_Sucursal: ID_Sucursal },

            success: function (response) {
                // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
                var data;
                var detalle;
                try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT




                    data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO


                    if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                    {

                        detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  

                        $("#TablaDetalleRecepcion tr>td").remove();




                        for (var i in detalle) {
                            StrTable = '<tr>'; // INICIO
                            StrTable = StrTable + '<td><a onclick="#">' + detalle[i].ID_Paquete + '</a></td>';
                            StrTable = StrTable + '<td><a onclick="#">' + detalle[i].ID_Orden_Transporte + '</a></td>';
                            StrTable = StrTable + '<td>' + detalle[i].Razon_Social + '</td>';
                            StrTable = StrTable + '<td>' + detalle[i].DescripcionRuta + '</td>';
                            StrTable = StrTable + '<td>' + detalle[i].DescripcionTipoCarga + '</td>';
                            StrTable = StrTable + '<td>' + detalle[i].DescripcionEmbalaje + '</td>';

                            StrTable = StrTable + '<td>';


                            var detalleGuias;

                            detalleGuias = jQuery.parseJSON(JSON.stringify(detalle[i].Guias));


                            for (var j in detalleGuias) {
                                StrTable = StrTable + '<a href=/guias/showUserPDF?IDGuias=' + detalleGuias[j].ID_Guia_Cliente + ' data-toggle="tooltip" title="' + detalleGuias[j].Numero_Guia + '" target="_blank"><i class="material-icons">description</i></a>';

                            }

                            StrTable = StrTable + '</td>';


                            StrTable = StrTable + '<td>' + detalle[i].Fecha_Ingreso + '</td>';
                            StrTable = StrTable + '<td>' + detalle[i].DescripcionSucursal + '</td>';
                            StrTable = StrTable + '<td>' + detalle[i].NombreUsuario + '</td>';

                       


                            //   StrTable = StrTable + '<td class="td-actions text-right"><button type="button" rel="tooltip" class="btn btn-danger" onclick="del_Bulto(this);return false;"><i class="material-icons">close</i></button></td>'; // BUTON
                            StrTable = StrTable + '</tr>'; // CIERRE



                            $('#TablaDetalleRecepcion tr').last().after(StrTable);
                         



                            i++;
                        }

                        //$('#TablaDetalleRecepcion').data.reload();
                    
                        $('.TablaDetalleRecepcion').each(function () {
                            dt = $(this).dataTable();
                            dt.fnDraw();
                        })

                         $('#ModalDetalleRecepcion').modal('show');

                        gfProceso();


                    }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                    else {



                        swal({
                            title: 'TOPTAINER BODEGA',
                            text: 'No existen Bultos en Slot de Bodega ' + data.Detalle_Error + '',
                            type: 'error',
                            confirmButtonColor: '#FE6A00',
                            confirmButtonText: 'OK',
                            confirmButtonClass: 'btn btn-info'
                        });


                        gfProceso();

                        return false;
                    }

                } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT


                    swal({
                        title: 'TOPTAINER BODEGA',
                        text: 'No existen Bultos en Slot de Bodega ' + data.Detalle_Error + '',
                        type: 'error',
                        confirmButtonColor: '#FE6A00',
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-info'
                    });
                    gfProceso();


                    return false;
                }



            },
            error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.
                alert('Error de Conexión al guardar la Orden de TRanspoete, Favor Revisar');

                gfProceso();
                return false;
            }
        });

    
}


function AgregarBodega(IdSlot) //  FUNCION QUE AGREGA LA VISTA DE BODEGAS.
{

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/Bodega/ConsultaSlotDescripcion",
        content: "application/json; charset=utf-8",
        dataType: "json",
        //   data: { Id_Cliente: Cliente, Id_Ruta: Ruta, Id_BaseCobro: BaseCobro, Id_TipoCarga: TipoCarga },
        data: { IdSlot: IdSlot },

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT



                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO


                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {
                    detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  



                    $("#LblNombreSlot").text(detalle.DescripcionSlot);

                    $("#LblIDSlot").text(IdSlot);


                    $('#ModalAgregarBodega').modal('show');



                    gfProceso();


                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {



                    swal({
                        title: 'ORDEN DE TRANSPORTE',
                        text: 'Error al buscar Slot en Bodega ' + data.Detalle_Error + '',
                        type: 'error',
                        confirmButtonColor: '#FE6A00',
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-info'
                    });


                    gfProceso();
                    return false;

                }

            } catch (err) { // ACCIÓN DEL ERROR DE JAVASCRIPT


                swal({
                    title: 'ORDEN DE TRANSPORTE',
                    text: 'Error al buscar Slot en Bodega ' + err + '',
                    type: 'error',
                    confirmButtonColor: '#FE6A00',
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-info'
                });

                gfProceso();
                return false;
            }



        },
        error: function (xhr, textStatus, errorThrown) { // ACCIÓN DEL ERROR DEL SERVICIO AJAX.
            alert('Error al buscar Slot en Bodega, Favor Revisar');

            gfProceso();
            return false;
        }
    });




} 



function redirectRecepcion()
{
    return window.location.href = '/OrdenTransporte/IngresoOrdenTransporte_Bodega';
}