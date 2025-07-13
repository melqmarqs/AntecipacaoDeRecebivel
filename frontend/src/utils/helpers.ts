export const Helpers = {
    cnpjFormatter(cnpj: string) {
        const digits = String(cnpj).replace(/\D/g, '');

        // Aplica a máscara apenas se tiver 14 dígitos
        if (digits.length !== 14) return cnpj.toString();

        return digits.replace(
            /^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/,
            '$1.$2.$3/$4-$5'
        );
    },
    cleanCNPJ(value: string | number): string {
        return String(value).replace(/\D/g, '');
    },
    formatCurrency(value: number, locale: string = 'pt-BR', currency: string = 'BRL'): string {
        return new Intl.NumberFormat(locale, {
            style: 'currency',
            currency: currency,
        }).format(value);
    },
    formatDate(date: Date, locale: string = 'pt-BR'): string {
        const parsedDate = new Date(date);

        if (isNaN(parsedDate.getTime())) {
            // Valor inválido
            return 'Data inválida';
        }

        return new Intl.DateTimeFormat(locale).format(parsedDate);
    },
    isFutureDay(date: Date): boolean {
        const today = new Date();
        today.setHours(0, 0, 0, 0);

        const input = new Date(date);
        input.setHours(0, 0, 0, 0);

        return input.getTime() > today.getTime();
    }
}