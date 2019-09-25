$(document).ready(function () {



    CargaComboRutas();
    CargaComboTipoTransporte();
    CargaComboChofer();
});





function AsignarCarga(ID_EquipoTransporte)

{

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/EquipoTransporte/ConsultaDetalleEquipoTransporte",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { ID_EquipoTransporte: ID_EquipoTransporte },


        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT




                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO

                //   alert(JSON.stringify(detalle));


                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR
                {


                    detalle = jQuery.parseJSON(data.Descripcion); // AQUI SE REVISA EL DETALLE


                //    alert(detalle);

                    $("#txtID_EquipoTransporte").val(detalle.ID_EquipoTransporte);
                    $("#CmbTipoTransporte").val(detalle.ID_Tipo_Transporte).change();
                    $("#txtPatente").val(detalle.Patente);
                    $('#ModalAgregarCarga').modal('show');
                    gfProceso();


                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {



                    swal({
                        title: 'TOPTAINER',
                        text: 'Error al buscar información de Equipo Transporte ' + data.Detalle_Error + '',
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
                    title: 'TOPTAINER',
                    text: 'Error al buscar información de Equipo Transporte' + data.Detalle_Error + '',
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
                title: 'TOPTAINER',
                text: 'Error de Conexión' + textStatus + '',
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




function Asignar_Carga(ID_OrdenViaje) {

    var ID_OrdenCarga;

    ID_OrdenCarga = $("#txt_ID_OrdenCarga").text();

  


    gfProceso();


    $.ajax({
        type: "POST",
        url: "/OrdenCarga/Ingresar_OrdenViaje_OrdenCarga",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: { ID_OrdenCarga: ID_OrdenCarga, ID_OrdenViaje: ID_OrdenViaje },


        success: function (response) {
            // FUNCION PARA VALIDAR AJAX Y RESPUESTA..
            var data;
            var detalle;
            try {  // AQUI SE CONTROLA EL ERROR DE JAVASCRIPT




                data = jQuery.parseJSON(JSON.stringify(response));  // AQUI SE REVISA LA RESPUESTA DEL SERVICIO

                //   alert(JSON.stringify(detalle));


                if (data.Respuesta == "OK") // AQUI MANEJA SI EL SERVICIO RETORNO OK O CONTROLES DE ERROR
                {



                    swal({
                        title: 'TOPTAINER',
                        text: "Orden de Viaje Ingresada con Éxito a Proceso de Carga",
                        type: 'success',
                        confirmButtonColor: '#4caf50',
                        //cancelButtonColor: '#d33',
                        confirmButtonText: 'OK',
                        onClose: function () {
                            window.location.href = "/OrdenCarga/OrdendeCarga";

                        }

                    });

                    gfProceso();


                }  // IF LA RESPUESTA DEL SERVICIO ES NOK ERROR CONTROLADO......
                else {



                    swal({
                        title: 'TOPTAINER',
                        text: 'Error Orden de Viaje Ingresada a Proceso de Carga ' + data.Detalle_Error + '',
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
                    title: 'TOPTAINER',
                    text: 'Error Orden de Viaje Ingresada a Proceso de Carga' + data.Detalle_Error + '',
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
                title: 'TOPTAINER',
                text: 'Error de Conexión' + textStatus + '',
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


function CargaComboRutas() {

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/Funciones/Combo_Rutas",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {},
        success: function (d) {

            var opt = '<option selected="selected" value="0">SELECCIONE RUTA</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });

            $('#CmbRutas').empty().append(opt);

            $('#CmbRutas').selectpicker('refresh');

            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
            gfProceso();
        }
    });

}

function CargaComboTipoTransporte() {

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/Funciones/Combo_TipoTransporte",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {},
        success: function (d) {

            var opt = '<option value="0">SELECCIONE TIPO TRANSPORTE</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;
                var Id_Sucursal = $("#Id_Sucursal").val();

                if (value == Id_Sucursal) {
                    opt = opt + '<option selected="selected"  value="' + value + '">' + text + '</option>';
                }
                else {
                    opt = opt + '<option value="' + value + '">' + text + '</option>';
                }
            });

            $('#CmbTipoTransporte').empty().append(opt);

            $('#CmbTipoTransporte').selectpicker('refresh');



            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
            gfProceso();
        }
    });

}

function CargaComboChofer() {

    gfProceso();


    $.ajax({
        type: "POST",
        url: "/Funciones/Combo_Chofer",
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: {},
        success: function (d) {

            var opt = '<option selected="selected" value="0">SELECCIONE CHOFER</option>';

            $.each(d, function (index, item) {
                var value = d[index].Value;
                var text = d[index].Text;

                opt = opt + '<option value="' + value + '">' + text + '</option>';
            });

            $('#CmbChofer').empty().append(opt);

            $('#CmbChofer').selectpicker('refresh');

            gfProceso();

        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
            gfProceso();
        }
    });

}







