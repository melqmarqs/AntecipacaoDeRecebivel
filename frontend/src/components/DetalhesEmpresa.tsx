import { RamoEnum } from "@/enums/ramo";
import { Empresa } from "@/models/empresa";
import { Helpers } from "@/utils/helpers";

export const DetalhesEmpresa: React.FC<{id: number}> = async ({id}) => {

    const response = await fetch('http://localhost:5201/api/empresa/' + id);
    const empresa: Empresa = await response.json();

    return (
        <div className="flex flex-col items-center">
            <span className="text-lg font-bold">
                {empresa.nome}
            </span>
            <span className="text-sm">
                {Helpers.cnpjFormatter(empresa.cnpj)}
            </span>
            <div className="flex flex-row items-center gap-5 text-sm">
                <span>
                    {Helpers.formatCurrency(empresa.faturamentoMensal)}
                </span>
                <span className="uppercase">
                    {RamoEnum[empresa.ramo]}
                </span>
            </div>
        </div>
    )
}