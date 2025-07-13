export interface NotaFiscal {
    id: number,
    empresaId: number,
    numero: string,
    valor: number,
    dataDeVencimento: Date,
    valorAntecipado: number,
    jaFoiAntecipada: boolean
}

export interface NotaFiscalRequest {
    empresaId: number,
    numero: string,
    valor: number,
    dataDeVencimento: Date
}

export interface NfAntecipacaoDto {
    numero: string,
    valorBruto: number,
    valorLiquido: number,
}