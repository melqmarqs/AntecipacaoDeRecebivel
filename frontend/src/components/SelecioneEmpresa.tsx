import { Empresa } from "@/models/empresa";
import { Helpers } from "@/utils/helpers";
import Link from "next/link";

export const SelecioneEmpresa: React.FC<{ empresas: Empresa[] }> = ({ empresas }) => {

    return (
        <div className="bg-red-300 h-screen">
            <div className="h-full flex flex-col items-center justify-center">
                <div className="flex flex-col items-center mb-3">
                    <span className="text-sm sm:text-base">
                        Selecione a companhia
                    </span>
                    <Link href='./empresa/criar' className="text-sm sm:text-base bg-blue-t active:bg-blue-act underline px-1 mt-1">
                        Nova empresa
                    </Link>
                </div>
                
                <div className="p-1 h-3/4 w-5/6 border-1 overflow-y-auto flex flex-col gap-2">
                {
                    empresas.map((e, i) => (
                    <Link href={`./empresa/${e.id}`} key={i} className="flex flex-col border-1 p-1">
                        <span className="sm:text-lg font-bold uppercase text-ellipsis text-nowrap overflow-x-hidden">
                            {e.nome}
                        </span>
                        <span className="text-base sm:text-lg text-ellipsis text-nowrap overflow-x-hidden">
                            {Helpers.cnpjFormatter(e.cnpj)}
                        </span>
                    </Link>
                    ))
                }
                </div>
            </div>
        </div>
    )
}