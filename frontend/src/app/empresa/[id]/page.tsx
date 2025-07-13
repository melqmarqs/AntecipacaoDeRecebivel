import { DetalhesEmpresa } from "@/components/DetalhesEmpresa";
import { NotasFiscaisEmpresa } from "@/components/NotasFiscaisEmpresa";

export default async function DetailEmpresa({params}: {params: Promise<{id: string}>}) {
    const { id } = await params;

    return (
        <div className="bg-red-300 w-screen h-screen flex flex-col items-center justify-center gap-1">
            <DetalhesEmpresa id={+id} />
            <div className="h-6/7 w-5/6">
                <NotasFiscaisEmpresa id={+id} />
            </div>
            
        </div>
        
    )
}