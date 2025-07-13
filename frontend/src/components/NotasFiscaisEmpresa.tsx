'use client'

import { NotaFiscal } from "@/models/notaFiscal";
import { Helpers } from "@/utils/helpers";
import { useEffect, useState } from "react";
import { useRouter } from 'next/navigation';
import Link from "next/link";
import { EmpresaAntecipacaoDto } from "@/models/empresa";

export const NotasFiscaisEmpresa: React.FC<{id: number}> = ({id}) => {
    const router = useRouter();
    const [notasFiscais, setNotasFiscais] = useState<NotaFiscal[]>([]);
    const [nfsSelecionadas, setNfsSelecionadas] = useState<number[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [antecipacao, setAntecipacao] = useState<EmpresaAntecipacaoDto | null>(null);

    useEffect(() => {
        async function getNFs() {
            try {
                const response = await fetch('http://localhost:5201/api/notafiscal/empresa/' + id);
                if (response.status === 200) {
                    const nfs = (await response.json()) as NotaFiscal[];
                    setNotasFiscais([...nfs]);
                }
            } catch {
                alert('Houve um problema com a requisição');
            }
        }

        getNFs();
    }, []);

    function handleClick(idNf: number) {
        if (nfsSelecionadas.includes(idNf)) {
            const temp = [...nfsSelecionadas];
            const novaLista = temp.filter(t => t != idNf);
            novaLista.length === 0 && setAntecipacao(null);
            setNfsSelecionadas([...novaLista]);
        } else {
            const novaLista = [...nfsSelecionadas];
            novaLista.push(idNf);
            calcularAntecipacao(novaLista);
        }
    };

    async function calcularAntecipacao(ids: number[]) {
        try {
            setIsLoading(true);

            const response = await fetch('http://localhost:5201/api/notafiscal/calcular', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(ids)
            });

            if (response.status === 200) {
                const temp = await response.json();
                setAntecipacao({...temp});
                setNfsSelecionadas([...ids]);
            } else if (response.status === 400) {
                const erro = await response.json();
                alert(erro.detail);
            }
        } catch {
            alert('Houve um problema com a requisição');
        } finally {
            setIsLoading(false);
        }
    };

    function redirecionarParaCriarNF() {
        router.push('../notaFiscal/' + id);
    };

    return (
        <div className="h-full flex flex-col justify-center gap-3">
            {
                isLoading ? (
                    <div className="flex flex-row items-center justify-center border-1 p-3">
                        <span className="uppercase">
                            carregando...
                        </span>
                    </div>
                ) : (
                    <>
                        {
                            antecipacao && (
                                <div className="flex flex-col items-start justify-center border-1 p-1">
                                    <span className="text-sm uppercase">
                                        Carrinho {nfsSelecionadas.length > 0 ? `(${nfsSelecionadas.length})` : null}
                                    </span>
                                    <div className="flex flex-row items-center justify-between w-full">
                                        <span className="text-sm">
                                            Valor bruto:
                                        </span>
                                        <span className="font-bold">
                                            {Helpers.formatCurrency(antecipacao.totalBruto)}
                                        </span>
                                    </div>
                                    <div className="flex flex-row items-center justify-between w-full">
                                        <span className="text-sm">
                                            Valor líquido:
                                        </span>
                                        <span className="font-bold">
                                            {Helpers.formatCurrency(antecipacao.totalLiquido)}
                                        </span>
                                    </div>
                                </div> 
                            )
                        }
                        
                        <div className="border-1 w-full h-5/7 overflow-y-auto">
                            {
                                notasFiscais.length > 0 ? (
                                    <>
                                        {
                                            notasFiscais.map(n => (
                                                <div key={`key${n.id}`}>
                                                    {
                                                        nfsSelecionadas.includes(n.id) ? (
                                                            <div key={n.id} onClick={_ => handleClick(n.id)} className="bg-blue-200 border-1 p-1 m-1 overflow-hidden flex flex-col items-center">
                                                                <span>
                                                                    #{n.numero}
                                                                </span>
                                                                <span>
                                                                    {Helpers.formatCurrency(n.valor)} | {Helpers.formatCurrency(antecipacao?.notasFiscais?.find(nf => nf.numero == n.numero)?.valorLiquido!)}
                                                                </span>
                                                                <span>
                                                                    {Helpers.formatDate(n.dataDeVencimento)}
                                                                </span>
                                                            </div>
                                                        ) : (
                                                            <div key={n.id} onClick={_ => handleClick(n.id)} className="border-1 p-1 m-1 overflow-hidden flex flex-col items-center">
                                                                <span>
                                                                    #{n.numero}
                                                                </span>
                                                                <span>
                                                                    {Helpers.formatCurrency(n.valor)}
                                                                </span>
                                                                <span>
                                                                    {Helpers.formatDate(n.dataDeVencimento)}
                                                                </span>
                                                            </div>
                                                        )
                                                    }
                                                </div>
                                            ))
                                        }
                                    </>
                                ) : (
                                    <div className="flex flex-col items-center">
                                        <span className="uppercase text-center">
                                            Essa empresa nao tem nenhuma nota fiscal registrada.
                                        </span>
                                    </div>
                                )
                            }

                            
                        </div>
                        <button className="bg-blue-t active:bg-blue-act cursor-pointer uppercase underline py-3" onClick={redirecionarParaCriarNF }>
                            adicionar nf
                        </button>
                        <Link href={'../'} className="bg-orange-500 uppercase underline text-center">
                            voltar
                        </Link>
                    </>
                )
            }
            
        </div>
    )
}