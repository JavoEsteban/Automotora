function validaMail(email) {
    var expresion = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return expresion.test(String(email).toLowerCase());
}
// CODIGO PARA PARTIR LA PAGINA WEB
// FUNCIONES SIMPLE SIN LOGICA DE NEGOCIOS


























