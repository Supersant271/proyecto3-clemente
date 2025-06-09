import { useEffect } from "react";

const Presentacion = () => {
  // Manejo de datos
const cargarDatos = async () => {
   const resp = await fetch("/mi-proyecto/presentacion");
   if(resp.ok){
     const datos = await resp.json();
     console.log (datos);
   }
}


  // Vista
  return (
    <>
      <div className="display-4">Nombre del cbtis</div>
      <div className="h1">Integrantes</div>
      <div className="h2">Nombre del integrante 1</div>
      <div className="h2">Nombre del integrante 2</div>
      <div className="h1">Nombre del proyecto</div>
    </>
  );
};

export default Presentacion;
