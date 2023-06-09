PGDMP     *    &                {            test    15.3    15.3     ;           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            <           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            =           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            >           1262    30933    test    DATABASE     {   CREATE DATABASE test WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Portuguese_Brazil.1252';
    DROP DATABASE test;
                postgres    false                        3079    30934    pgcrypto 	   EXTENSION     <   CREATE EXTENSION IF NOT EXISTS pgcrypto WITH SCHEMA public;
    DROP EXTENSION pgcrypto;
                   false            ?           0    0    EXTENSION pgcrypto    COMMENT     <   COMMENT ON EXTENSION pgcrypto IS 'cryptographic functions';
                        false    2            s           1247    30973 	   type_cars    TYPE     �   CREATE TYPE public.type_cars AS (
	id integer,
	name character varying,
	color character varying,
	price money,
	qtpassengers integer,
	marca character varying
);
    DROP TYPE public.type_cars;
       public          postgres    false                       1255    30974 
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
			b.name
		FROM
			cars c
		LEFT JOIN
			brands b ON b.id = c.codmarca
		ORDER BY c.id ASC;
END
$$;
 !   DROP FUNCTION public.get_cars();
       public          postgres    false                       1255    30975    insertbrand(character varying)    FUNCTION     �   CREATE FUNCTION public.insertbrand(brandname character varying) RETURNS character varying
    LANGUAGE plpgsql
    AS $$BEGIN
	INSERT INTO brands (name)
    VALUES (TRIM(UPPER(brandname))); 
	RETURN 'Success!';
END $$;
 ?   DROP FUNCTION public.insertbrand(brandname character varying);
       public          postgres    false                       1255    30976 \   insertcar(character varying, character varying, double precision, integer, integer, integer)    FUNCTION     �  CREATE FUNCTION public.insertcar(namecar character varying, colorcar character varying, pricecar double precision, yearcar integer, qtpassengerscar integer, codmarcacar integer) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO cars (name, color, price, year, qtpassengers, codmarca)
    VALUES (TRIM(UPPER(namecar)), TRIM(UPPER(colorcar)), pricecar, yearcar, qtpassengerscar, codmarcacar);
    RETURN 'Success';
END 
$$;
 �   DROP FUNCTION public.insertcar(namecar character varying, colorcar character varying, pricecar double precision, yearcar integer, qtpassengerscar integer, codmarcacar integer);
       public          postgres    false            �            1259    30977    brands    TABLE     T   CREATE TABLE public.brands (
    id integer NOT NULL,
    name character varying
);
    DROP TABLE public.brands;
       public         heap    postgres    false            �            1259    30982    cars    TABLE     �   CREATE TABLE public.cars (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    color character varying(25) NOT NULL,
    price numeric NOT NULL,
    year integer,
    qtpassengers integer,
    codmarca integer DEFAULT 1 NOT NULL
);
    DROP TABLE public.cars;
       public         heap    postgres    false            �            1259    30988    cars_id_seq    SEQUENCE     �   CREATE SEQUENCE public.cars_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.cars_id_seq;
       public          postgres    false    217            @           0    0    cars_id_seq    SEQUENCE OWNED BY     ;   ALTER SEQUENCE public.cars_id_seq OWNED BY public.cars.id;
          public          postgres    false    218            �            1259    30989    marcas_id_seq    SEQUENCE     �   CREATE SEQUENCE public.marcas_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.marcas_id_seq;
       public          postgres    false    216            A           0    0    marcas_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.marcas_id_seq OWNED BY public.brands.id;
          public          postgres    false    219            �            1259    30990    users    TABLE     �   CREATE TABLE public.users (
    id integer NOT NULL,
    login character varying NOT NULL,
    password character varying NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false            �            1259    30995    users_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.users_id_seq;
       public          postgres    false    220            B           0    0    users_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;
          public          postgres    false    221            �           2604    30996 	   brands id    DEFAULT     f   ALTER TABLE ONLY public.brands ALTER COLUMN id SET DEFAULT nextval('public.marcas_id_seq'::regclass);
 8   ALTER TABLE public.brands ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    219    216            �           2604    30997    cars id    DEFAULT     b   ALTER TABLE ONLY public.cars ALTER COLUMN id SET DEFAULT nextval('public.cars_id_seq'::regclass);
 6   ALTER TABLE public.cars ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    218    217            �           2604    30998    users id    DEFAULT     d   ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);
 7   ALTER TABLE public.users ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    221    220            3          0    30977    brands 
   TABLE DATA           *   COPY public.brands (id, name) FROM stdin;
    public          postgres    false    216   �"       4          0    30982    cars 
   TABLE DATA           T   COPY public.cars (id, name, color, price, year, qtpassengers, codmarca) FROM stdin;
    public          postgres    false    217   �#       7          0    30990    users 
   TABLE DATA           4   COPY public.users (id, login, password) FROM stdin;
    public          postgres    false    220   �#       C           0    0    cars_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.cars_id_seq', 1, false);
          public          postgres    false    218            D           0    0    marcas_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.marcas_id_seq', 30, true);
          public          postgres    false    219            E           0    0    users_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.users_id_seq', 1, false);
          public          postgres    false    221            �           2606    31000    cars cars_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY public.cars
    ADD CONSTRAINT cars_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.cars DROP CONSTRAINT cars_pkey;
       public            postgres    false    217            �           2606    31002    brands marcas_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.brands
    ADD CONSTRAINT marcas_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.brands DROP CONSTRAINT marcas_pkey;
       public            postgres    false    216            �           2606    31004    users users_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    220            3   �   x��[�� D�{V�
n	���I�H,-������8]m��.�j%�EJ�h@�4i�kȁF���^2��ntƔ^tA�2��]1������M�!qi!�9b���$�8&c�m���C�Jfē=�1sǅE�t���xѭ��`��k_;�J���7��o\���y!k�����Z���6�����s�-;�����:�+g�E�d/�مy&�w~��Mjd�ȡV���#��'F�      4      x������ � �      7      x������ � �     