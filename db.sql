-- public.food definition

-- Drop table

-- DROP TABLE public.food;

CREATE TABLE public.food (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	"name" varchar(300) NOT NULL,
	kkal float4 NOT NULL,
	description varchar(400) NULL,
	protein float4 NULL,
	fats float4 NULL,
	carbohydrates float4 NULL,
	CONSTRAINT food_pk PRIMARY KEY (id),
	CONSTRAINT food_un UNIQUE (name)
);


-- public.typesport definition

-- Drop table

-- DROP TABLE public.typesport;

CREATE TABLE public.typesport (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	"name" varchar(200) NOT NULL,
	description varchar(500) NULL,
	CONSTRAINT typesport_pk PRIMARY KEY (id),
	CONSTRAINT typesport_un UNIQUE (name)
);


-- public.users definition

-- Drop table

-- DROP TABLE public.users;

CREATE TABLE public.users (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	login varchar(150) NOT NULL,
	hash varchar(40) NOT NULL,
	salt varchar(40) NOT NULL,
	firstname varchar(150) NOT NULL,
	lastname varchar(150) NOT NULL,
	midname varchar(150) NULL,
	sex bpchar(1) NULL,
	datebridh date NULL,
	height float4 NULL,
	CONSTRAINT users_check CHECK ((sex = ANY (ARRAY['m'::bpchar, 'f'::bpchar]))),
	CONSTRAINT users_pk PRIMARY KEY (id),
	CONSTRAINT users_un UNIQUE (login)
);


-- public.eating definition

-- Drop table

-- DROP TABLE public.eating;

CREATE TABLE public.eating (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	datatime timestamp NOT NULL,
	weight float4 NOT NULL,
	idusers int8 NOT NULL,
	idfood int8 NOT NULL,
	CONSTRAINT eating_pk PRIMARY KEY (id),
	CONSTRAINT eating_fk FOREIGN KEY (idusers) REFERENCES users(id) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT eating_fk2 FOREIGN KEY (idfood) REFERENCES food(id)
);


-- public.sports definition

-- Drop table

-- DROP TABLE public.sports;

CREATE TABLE public.sports (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	att1 float4 NULL,
	att2 float4 NULL,
	att3 float4 NULL,
	att4 float4 NULL,
	idusers int8 NOT NULL,
	idsports int8 NOT NULL,
	CONSTRAINT sports_pk PRIMARY KEY (id),
	CONSTRAINT sports_fk FOREIGN KEY (idusers) REFERENCES users(id) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT sports_fk2 FOREIGN KEY (idsports) REFERENCES typesport(id) ON UPDATE CASCADE
);


-- public.weight definition

-- Drop table

-- DROP TABLE public.weight;

CREATE TABLE public.weight (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	datatime timestamp NOT NULL,
	value float4 NOT NULL,
	idusers int8 NOT NULL,
	CONSTRAINT weight_pk PRIMARY KEY (id),
	CONSTRAINT weight_fk FOREIGN KEY (idusers) REFERENCES users(id)
);
