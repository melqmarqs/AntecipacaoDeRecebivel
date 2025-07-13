import { AdicionarNF } from "@/components/AdicionarNF";

export default async function CriarNF({params}: {params: Promise<{empresaId: string}>}) {
    const { empresaId } = await params;

    return (
        <AdicionarNF empresaId={+empresaId} />
    )
}