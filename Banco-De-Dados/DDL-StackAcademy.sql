CREATE DATABASE StackAcademy;

GO

USE StackAcademy;

GO

CREATE TABLE Aluno(
	-- Id do aluno em específico
	IdAluno UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	-- Nome do aluno, não pode aceitar vazio
	Nome VARCHAR(255) NOT NULL,
	-- Email do aluno, não pode ficar vazio e deve ser único
	Email NVARCHAR(255) NOT NULL UNIQUE,
	-- Senha do aluno, não pode aceitar vazio
	Senha NVARCHAR(40) NOT NULL,
    -- CPF do aluno, não pode ficar vazio e deve ser único
	Cpf NVARCHAR(14) NOT NULL UNIQUE,
	-- Imagem do aluno, ele pode deixar vazio
	Imagem VARCHAR(100),

	senha NVARCHAR(40) NOT NULL
);

GO

CREATE TABLE Professor(
	-- Id do professor em específico
	IdProfessor UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	-- Nome do professor, não pode aceitar vazio
	Nome VARCHAR(255) NOT NULL,
	-- Email do professor, não pode ser vazio e deve ser único
	Email NVARCHAR(255) NOT NULL UNIQUE,
	-- Senha do professor, não deve aceitar vazio
	Senha NVARCHAR(40) NOT NULL,
	-- CPF do professor, não pode ser vazio e deve ser único
	Cpf NVARCHAR(14) NOT NULL UNIQUE,
	-- Imagem do professor, ele não pode ser vazio
	Imagem VARCHAR(100) NOT NULL,

	senha NVARCHAR(40) NOT NULL
);

GO

CREATE TABLE Categoria(
	-- Id da categoria do curso em específico
	IdCategoria UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	-- Nome da categoria do curso em específico
	Nome NVARCHAR(255) NOT NULL
);

GO

CREATE TABLE Curso(
	-- Id do curso em específico
	IdCurso UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	-- Nome do curso em específico
	Nome NVARCHAR(255) NOT NULL,
	-- Data do curso, quando ele se iniciará
	DataInicio DATETIME NOT NULL,
	-- Data do curso, quando ele será encerrado
	DataFim DATETIME NOT NULL,



	-- Puxa a categoria no qual ele está cadastrado
	IdCategoria UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Categoria(IdCategoria),
	IdProfessor UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Professor(IdProfessor)
);

GO

-- Tabela intermediária de Aluno e Curso
CREATE TABLE AlunoCurso(

	-- Id do Aluno que está realizando o curso
	IdAluno UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Aluno(IdAluno),
	-- Id do curso que o aluno está realizando
	IdCurso UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Curso(IdCurso),
	-- Id do aluno e curso que estão entrelaçados
	PRIMARY KEY(IdAluno, IdCurso)
);

