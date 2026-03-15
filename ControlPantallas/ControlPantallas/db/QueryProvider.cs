namespace SuperfaldonWinUI.Data
{
    public static class QueryProvider
    {

        public static string GetAllCircunscripciones =>
            @"SELECT
                circunscripcion AS Codigo,
                comunidad AS Comunidad,
                provincia AS Provincia,
                municipio AS Municipio,
                descripcion AS Descripcion,
                escanos AS Escanos,
                escrutado AS Escrutado,
                participacion AS Participacion,
                participacion_hist AS ParticipacionH,
                avance1 AS Avance1,
                avance2 AS Avance2,
                avance3 AS Avance3,
                avance1_hist AS Avance1H,
                avance2_hist AS Avance2H,
                avance3_hist AS Avance3H
            FROM circunscripciones";


        public static string GetAllPartidos =>
            @"SELECT 
                PARTIDO     AS Codigo,
                descripcion  AS Nombre,
                sigla  AS Siglas
            FROM partidos";

        public static string GetPartidoByCodigo =>
    @"SELECT 
        cod_partido     AS Codigo,
        nombre_partido  AS Nombre,
        siglas_partido  AS Siglas,
        color_hex       AS Color
      FROM circunscripcion_partido
      WHERE cod_partido = @Codigo";


        public static string GetAllPartidosByCircunscripcion =>
            @"SELECT
                cp.cod_partido AS Codigo,
                cp.escanos_desde AS EscanosDesde,
                cp.escanos_hasta AS EscanosHasta,
                cp.escanos_desde_hist AS EscanosDesdeH,
                cp.escanos_hasta_hist AS EscanosHastaH,
                cp.votos AS VotosP,
                cp.votantes AS Votos,
                cp.votos_hist AS VotosPH,
                cp.votantes_hist AS VotosH,
                p.descripcion AS Nombre,
                p.sigla AS Siglas
            FROM circunscripcion_partido cp
            INNER JOIN partidos p 
                ON cp.cod_partido = p.PARTIDO
            WHERE cod_circunscripcion = @CodigoCircunscripcion and cp.escanos_desde > 0";

        public static string GetCircunscripcionByCodigo =>
            @"SELECT
                cod_circunscripcion AS Codigo,
                comunidad AS Comunidad,
                provincia AS Provincia,
                municipio AS Municipio,
                descripcion AS Descripcion,
                escanos AS Escanos,
                escrutado AS Escrutado,
                participacion AS Participacion,
                participacion_hist AS ParticipacionH
                avance1 AS Avance1,
                avance2 AS Avance2,
                avance3 AS Avance3
            FROM circunscripciones
            WHERE cod_circunscripcion = @Codigo";

        public static string GetPartidosByCircunscripcion { get; internal set; }
    }
}
