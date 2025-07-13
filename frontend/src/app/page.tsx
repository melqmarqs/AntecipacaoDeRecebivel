import { Iniciar } from "@/components/Iniciar";
import { SelecioneEmpresa } from "@/components/SelecioneEmpresa";
import { Empresa } from "@/models/empresa";

export default async function Home() {
  let temEmpresa = false;
  let data: Empresa[] = [];

  try {
    const response = await fetch('http://localhost:5201/api/empresa/');
    data = await response.json();
    temEmpresa = data.length > 0;
  } catch (error) {
    
  }
  

  return (
    <>
      {
        temEmpresa ? (
          <SelecioneEmpresa empresas={data} />
        ) : (
          <Iniciar />
        )
      }
    </>
  );
}
