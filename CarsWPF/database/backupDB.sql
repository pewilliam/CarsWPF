PGDMP         /                {            test    15.3    15.3                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    30860    test    DATABASE     {   CREATE DATABASE test WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Portuguese_Brazil.1252';
    DROP DATABASE test;
                postgres    false            K           1247    30863 	   type_cars    TYPE     �   CREATE TYPE public.type_cars AS (
	id integer,
	name character varying,
	color character varying,
	price money,
	qtpassengers integer,
	marca character varying
);
    DROP TYPE public.type_cars;
       public          postgres    false            �            1255    30864 
   get_cars()    FUNCTION     �  CREATE FUNCTION public.get_cars() RETURNS TABLE(id integer, name character varying, color character varying, price numeric, year integer, qtpassenger integer, marca character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
		SELECT
			c.id,
			c.name,
			c.color,
			c.price,
			c.year,
			c.qtpassengers,
			b.nome
		FROM
			cars c
		LEFT JOIN
			brands b ON b.id = c.codmarca
		ORDER BY c.id ASC;
END
$$;
 !   DROP FUNCTION public.get_cars();
       public          postgres    false            �            1255    30865 \   insertcar(character varying, character varying, double precision, integer, integer, integer)    FUNCTION     �  CREATE FUNCTION public.insertcar(namecar character varying, colorcar character varying, pricecar double precision, yearcar integer, qtpassengerscar integer, codmarcacar integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO cars (name, color, price, year, qtpassengers, codmarca)
    VALUES (TRIM(UPPER(namecar)), TRIM(UPPER(colorcar)), pricecar, yearcar, qtpassengerscar, codmarcacar);
    RETURN 'Success';
END 
$$;
 �   DROP FUNCTION public.insertcar(namecar character varying, colorcar character varying, pricecar double precision, yearcar integer, qtpassengerscar integer, codmarcacar integer);
       public          postgres    false            �            1259    30866    brands    TABLE     T   CREATE TABLE public.brands (
    id integer NOT NULL,
    nome character varying
);
    DROP TABLE public.brands;
       public         heap    postgres    false            �            1259    30871    cars    TABLE     �   CREATE TABLE public.cars (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    color character varying(25) NOT NULL,
    price numeric NOT NULL,
    year integer,
    qtpassengers integer,
    codmarca integer DEFAULT 1 NOT NULL
);
    DROP TABLE public.cars;
       public         heap    postgres    false            �            1259    30877    cars_id_seq    SEQUENCE     �   CREATE SEQUENCE public.cars_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.cars_id_seq;
       public          postgres    false    216                       0    0    cars_id_seq    SEQUENCE OWNED BY     ;   ALTER SEQUENCE public.cars_id_seq OWNED BY public.cars.id;
          public          postgres    false    217            �            1259    30878    marcas_id_seq    SEQUENCE     �   CREATE SEQUENCE public.marcas_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.marcas_id_seq;
       public          postgres    false    215                       0    0    marcas_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.marcas_id_seq OWNED BY public.brands.id;
          public          postgres    false    218            p           2604    30879 	   brands id    DEFAULT     f   ALTER TABLE ONLY public.brands ALTER COLUMN id SET DEFAULT nextval('public.marcas_id_seq'::regclass);
 8   ALTER TABLE public.brands ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    218    215            q           2604    30880    cars id    DEFAULT     b   ALTER TABLE ONLY public.cars ALTER COLUMN id SET DEFAULT nextval('public.cars_id_seq'::regclass);
 6   ALTER TABLE public.cars ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    217    216                      0    30866    brands 
   TABLE DATA           *   COPY public.brands (id, nome) FROM stdin;
    public          postgres    false    215   _                 0    30871    cars 
   TABLE DATA           T   COPY public.cars (id, name, color, price, year, qtpassengers, codmarca) FROM stdin;
    public          postgres    false    216   '                  0    0    cars_id_seq    SEQUENCE SET     9   SELECT pg_catalog.setval('public.cars_id_seq', 9, true);
          public          postgres    false    217                       0    0    marcas_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.marcas_id_seq', 22, true);
          public          postgres    false    218            v           2606    30882    cars cars_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY public.cars
    ADD CONSTRAINT cars_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.cars DROP CONSTRAINT cars_pkey;
       public            postgres    false    216            t           2606    30884    brands marcas_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.brands
    ADD CONSTRAINT marcas_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.brands DROP CONSTRAINT marcas_pkey;
       public            postgres    false    215               �   x��K��0�z��2����3vD�N����� l_u�c�yUo�hTw�#��y��q���o{I�g�t\h�/\��?4�7zL������n��<�4�G0S�wp�b}i��7��DOI��񙤌�s���zjR[:�J��_��e���ӏ�E�@�bR�n�!Pͽ-CnS�����6f         �   x�M�K� ���p�{�!Z��4Ħ6q��>#C���{L1�<�Ūz �B�6�J���>&\'�4�!L%����	�jwW���>�Jf�EÂY���ງR��}Nc�xG8�Wrhs�h�Мم<��!��jk�WS�1ӵ�_�[��*�3� 	�1�     