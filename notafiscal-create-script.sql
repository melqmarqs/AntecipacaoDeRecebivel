-- Table: public.NotasFiscais

-- DROP TABLE IF EXISTS public."NotasFiscais";

CREATE TABLE IF NOT EXISTS public."NotasFiscais"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "EmpresaId" integer NOT NULL,
    "Numero" character varying(9) COLLATE pg_catalog."default" NOT NULL,
    "Valor" numeric(18,2) NOT NULL,
    "DataDeVencimento" date NOT NULL,
    "ValorAntecipado" numeric(18,2) NOT NULL,
    "JaFoiAntecipada" boolean NOT NULL,
    "IsDeleted" boolean NOT NULL,
    CONSTRAINT "PK_NotasFiscais" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_NotasFiscais_Empresas_EmpresaId" FOREIGN KEY ("EmpresaId")
        REFERENCES public."Empresas" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."NotasFiscais"
    OWNER to postgres;
-- Index: IX_NotasFiscais_EmpresaId

-- DROP INDEX IF EXISTS public."IX_NotasFiscais_EmpresaId";

CREATE INDEX IF NOT EXISTS "IX_NotasFiscais_EmpresaId"
    ON public."NotasFiscais" USING btree
    ("EmpresaId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_NotasFiscais_Numero

-- DROP INDEX IF EXISTS public."IX_NotasFiscais_Numero";

CREATE UNIQUE INDEX IF NOT EXISTS "IX_NotasFiscais_Numero"
    ON public."NotasFiscais" USING btree
    ("Numero" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;