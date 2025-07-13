import Link from "next/link";

export function Iniciar () {
    return (
        <div className="bg-red-300 h-screen flex flex-col items-center justify-center">
            <span className="uppercase flex flex-col items-center text-base sm:text-xl/10">
                Importe os dados de uma
                <span className="text-2xl md:text-4xl font-bold">
                    empresa
                </span>
            </span>
            <Link href="./empresa/criar" className="sm:mt-3 uppercase underline bg-blue-t active:bg-blue-act text-2xl md:text-4xl font-bold px-5 py-1">
                Começar
            </Link>
        </div>
    )
}