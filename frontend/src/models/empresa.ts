import { RamoEnum } from "@/enums/ramo";
import { NfAntecipacaoDto, NotaFiscal } from "./notaFiscal";

export interface Empresa {
    id: number,
    nome: string,
    cnpj: string,
    faturamentoMensal: number,
    ramo: RamoEnum
    notasFiscais: NotaFiscal[]
}

export interface EmpresaRequest {
    nome: string,
    cnpj: string,
    faturamentoMensal: number,
    ramo: number
}

export interface EmpresaAntecipacaoDto {
    empresa: string,
    cnpj: string,
    limite: number,
    totalLiquido: number,
    totalBruto: number,
    notasFiscais: NfAntecipacaoDto[]
}