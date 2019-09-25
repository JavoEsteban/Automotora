function VerOrdenViajeEmitida() {

   
    gfProceso();




    $.ajax({
        type: "POST",
        url: "/OrdenViaje/ConsultaOrdenViajeEmitida",
        content: "application/json; charset=utf-8",
        dataType: "json",
        //   data: { Id_Cliente: Cliente, Id_Ruta: Ruta, Id_BaseCobro: BaseCobro, Id_TipoCarga: TipoCarga },
        data: { },

        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT




                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO
                detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE  

             //   alert(JSON.stringify(detalle));


                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR 
                {

                    $("#TblOrdenesdeViaje tr>td").remove();




                    for (var i in detalle) {
                        StrTable = '<tr>'; // INICIO
                        StrTable = StrTable + '<td><a onclick="#">' + detalle[i].ID_OrdenViaje + '</a></td>';
                        StrTable = StrTable + '<td><a onclick="#">' + detalle[i].Razon_Social + '</a></td>';
                        StrTable = StrTable + '<td>' + detalle[i].Ruta + '</td>';
                        StrTable = StrTable + '<td>' + detalle[i].Fecha_Ingreso + '</td>';
                        StrTable = StrTable + '<td>' + detalle[i].BasedeCobro + '</td>';
                        StrTable = StrTable + '<td>' + detalle[i].TipodeCarga + '</td>';

                      
                        StrTable = StrTable + '<td>' + detalle[i].TotalNeto + '</td>';
                    
                        StrTable = StrTable + '<td>' + detalle[i].Usuario + '</td>';
                        StrTable = StrTable + '<td> <a href="javascript:VerOrdenTransporte_OV(' + detalle[i].ID_OrdenViaje + ');"> <i class="material-icons">insert_drive_file</i></a></td>';
                        StrTable = StrTable + '<td><i class="material-icons">rv_hookup</i></td>';


                        //   StrTable = StrTable + '<td class="td-actions text-right"><button type="button" rel="tooltip" class="btn btn-danger" onclick="del_Bulto(this);return false;"><i class="material-icons">close</i></button></td>'; // BUTON
                        StrTable = StrTable + '</tr>'; // CIERRE

                        $('#lblTituloSlot').text(detalle[0].DescripcionSlot);


                        $('#TblOrdenesdeViaje tr').last().after(StrTable);
                        i++;
                    }






                    $('#myModal').modal('show');




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
