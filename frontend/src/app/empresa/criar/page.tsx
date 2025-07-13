'use client'

import { RamoEnum } from "@/enums/ramo";
import { EmpresaRequest } from "@/models/empresa";
import { Helpers } from "@/utils/helpers";
import { FormEvent, useState } from "react";
import { useRouter } from 'next/navigation';

export default function CriarEmpresa() {
    const router = useRouter();
    const ramoEnumValues = Object.values(RamoEnum).filter(r => !isNaN(+r));
    const [nomeEmpresa, setNomeEmpresa] = useState<string>('');
    const [cnpj, setCnpj] = useState<string>('');
    const [ramo, setRamo] = useState<RamoEnum>(1);
    const [faturamento, setFaturamento] = useState<string>('');

    async function handleFormAction(event: FormEvent) {
        event.preventDefault();

        if (!validarCampos())
            return;

        try {
            const req: EmpresaRequest = {
                nome: nomeEmpresa,
                cnpj: cnpj,
                faturamentoMensal: Number(faturamento),
                ramo: +ramo!
            };

            const response = await fetch('http://localhost:5201/api/empresa', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(req)
            });

            if (response.status == 400)
                alert('Verifique os campos e tente novamente');
            else
                router.push('/');
        } catch (error) {
            alert('Erro na requisição');
        }
    }

    function validarCampos(): boolean {
        let camposComErros: string[] = [];
        if (nomeEmpresa.length <= 0)
            camposComErros.push('Nome da empresa');

        if (Helpers.cleanCNPJ(cnpj).length != 14)
            camposComErros.push('CNPJ');

        if (+faturamento <= 0)
            camposComErros.push('Faturamento mensal');

        if (!ramo)
            camposComErros.push('Ramo');

        if (camposComErros.length) {
            let msgErro = camposComErros.join(', ');
            alert('Erro nos campos: ' + msgErro);
            return false;
        }

        return true;
    }

    return (
        <div className="sm:text-xl bg-red-300 h-screen w-screen flex flex-col items-center justify-center">
            <form className="flex flex-col gap-4" onSubmit={handleFormAction}>
                <div className="flex flex-col">
                    <span>
                        Nome da empresa
                    </span>
                    <input
                        type="text"
                        value={nomeEmpresa}
                        onChange={(e) => setNomeEmpresa(e.target.value)}
                        name="nome"
                        className="border-1 py-1 px-1" />
                </div>
                
                <div className="flex flex-col">
                    <span>
                        CNPJ
                    </span>
                    <input
                        type="text"
                        maxLength={18}
                        value={cnpj}
                        onChange={e => setCnpj(e.target.value)}
                        className="border-1 py-1 px-1" />
                </div>

                <div className="flex flex-col">
                    <span>
                        Faturamento mensal (R$)
                    </span>
                    <input
                        type="number"
                        value={faturamento}
                        onChange={e => setFaturamento(e.target.value)}
                        className="border-1 py-1 px-1" />
                </div>

                <div className="flex flex-col">
                    <span>
                        Ramo
                    </span>
                    <select value={ramo} onChange={e => setRamo(+(e.target.value) as RamoEnum)} name="ramo" id="ramo" className="border-1 py-1 px-1">
                        {
                            ramoEnumValues.map(r => (
                                <option key={r} value={r}>{RamoEnum[+r]}</option>
                            ))
                        }
                    </select>
                </div>
                <button type="submit" className="bg-blue-t active:bg-blue-act uppercase underline py-3 cursor-pointer">
                    cadastrar
                </button>
            </form>
        </div>
    )
}