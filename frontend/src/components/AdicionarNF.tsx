'use client'

import { NotaFiscalRequest } from "@/models/notaFiscal";
import { Helpers } from "@/utils/helpers";
import { FormEvent, useState } from "react"
import { useRouter } from 'next/navigation';
import Link from "next/link";

export const AdicionarNF: React.FC<{empresaId: number}> = ({empresaId}) => {
    const router = useRouter();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [numeroNF, setNumeroNF] = useState<string>('');
    const [valor, setValor] = useState<string>('');
    const [venc, setVenc] = useState<string>('');

    async function handleAdicionarNF(event: FormEvent) {
        event.preventDefault();
        
        if (!validarCampos())
            return;

        try {
            setIsLoading(true);

            var req: NotaFiscalRequest = {
                empresaId: empresaId,
                numero: numeroNF,
                valor: +valor,
                dataDeVencimento: new Date(venc + 'T00:00:00')
            };

            const response = await fetch('http://localhost:5201/api/notafiscal', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(req)
            });

            if (response.status === 204)
                router.push('../empresa/' + empresaId);

            console.log('-', numeroNF, valor, venc);
        } catch {
            
        } finally {
            setIsLoading(false);
        }
    }

    function validarCampos(): boolean {
        const camposComErros: string[] = [];
        if (numeroNF.length != 9 || isNaN(+numeroNF))
            camposComErros.push('Número');

        if (valor.length <= 0 || isNaN(+valor) || +valor <= 0)
            camposComErros.push('Valor');

        const dataNota = new Date(venc + 'T00:00:00')
        if (!Helpers.isFutureDay(dataNota))
            camposComErros.push('Vencimento');

        if (camposComErros.length) {
            var erroMsg = camposComErros.join(', ');
            alert('Campos com erros: ' + erroMsg);
            return false;
        }

        return true;
    }

    return (
        <div className="flex flex-col items-center justify-center h-screen bg-red-300">
            {
                isLoading ? (
                    <div className="border-1 py-3 px-4">
                        <span className="uppercase text-lg">carregando...</span>
                    </div>
                ) : (
                    <>
                        <span className="uppercase text-xl font-bold mb-8">NOTA FISCAL</span>
                        <form className="flex flex-col gap-4 w-7/10" onSubmit={handleAdicionarNF}>
                            <div className="flex flex-col items-start">
                                <span className="uppercase">Número</span>
                                <input
                                    className="border-1 p-1 w-full"
                                    maxLength={9}
                                    type="text"
                                    name="numeroNf"
                                    id="numeroNf"
                                    value={numeroNF}
                                    onChange={e => setNumeroNF(e.target.value)} />
                            </div>
                            <div className="flex flex-col items-start">
                                <span className="uppercase">valor</span>
                                <input
                                    className="border-1 p-1 w-full"
                                    type="number"
                                    name="valor"
                                    id="valor"
                                    value={valor}
                                    onChange={e => setValor(e.target.value)} />
                            </div>
                            <div className="flex flex-col items-start">
                                <span className="uppercase">vencimento</span>
                                <input
                                    className="border-1 p-1 w-full"
                                    type="date"
                                    name="venc"
                                    id="venc"
                                    value={venc}
                                    onChange={e => setVenc(e.target.value)} />
                            </div>
                            <button type="submit" className="bg-blue-t active:bg-blue-act uppercase underline py-2 mt-2">
                                adicionar
                            </button> 
                            <Link href={'../empresa/' + empresaId} className="bg-orange-500 uppercase underline text-center">
                                voltar
                            </Link>
                        </form> 
                    </>
                   
                )
            }
        </div>
    )
}