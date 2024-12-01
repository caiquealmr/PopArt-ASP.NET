# **Projeto PopArt - ASP.NET**

- **Nome do Aluno:** Caíque Almeida Amaral  
- **Matrícula:** 202305874  
- **Turma:** SI4N  
- **Nome da Professora:** Gabriela Martins de Jesus  

> Atividade Avaliativa realizada na disciplina de **Programação Orientada a Objetos II** durante o **quarto período** do curso de **Sistemas de Informação** na **Universidade Vila Velha**.

---

## **Participantes**

O **Projeto PopArt** foi desenvolvido em uma dinâmica de grupo. Este repositório reflete o esforço conjunto dos seguintes integrantes:

- **[Caíque Almeida Amaral](https://github.com/caiquealmr)**  
- **[Manoel Furtado Gomes Neto](https://github.com/oManoelNeto)**  
- **[Mateus Sarmento da Hora](https://github.com/sarmentin)**  

---

## **O Projeto**

**PopArt** é uma rede social dedicada tanto a artistas quanto a amantes de arte. A plataforma permite que artistas compartilhem suas obras, conectem-se com outros criadores e encontrem novos admiradores. Ao mesmo tempo, os amantes de arte podem explorar, apreciar e interagir com conteúdos artísticos. A interface é projetada para promover um ambiente criativo e colaborativo entre esses públicos.

---

## **Tecnologias Utilizadas**

O **PopArt** foi desenvolvido utilizando as seguintes tecnologias:

| Tecnologia           | Descrição                                                                 |
|-----------------------|---------------------------------------------------------------------------|
| **C#**               | Linguagem de programação principal utilizada para o desenvolvimento.      |
| **ASP.NET Core MVC**  | Framework utilizado para estruturar a aplicação com o padrão MVC.        |
| **SQL Server**        | Banco de dados relacional utilizado para armazenar as informações.       |
| **Entity Framework**  | ORM utilizado para mapear objetos para o banco de dados.                 |
| **HTML, CSS, JavaScript** | Tecnologias usadas para criar o frontend da aplicação.               |
| **Git/GitHub**        | Controle de versão e repositório remoto para colaboração no projeto.      |

---

## **Banco de Dados - SQL Server**

O projeto utiliza o **SQL Server** como banco de dados, devido à sua alta performance, confiabilidade e facilidade de integração com o ASP.NET Core.  

A estrutura do banco foi criada utilizando o Entity Framework Core para realizar mapeamento objeto-relacional (ORM), facilitando a criação e manipulação de tabelas diretamente a partir dos modelos da aplicação.

---

# **Como Rodar a Aplicação PopArt**

### **Pré-requisitos**

Antes de executar o projeto, certifique-se de ter os seguintes itens instalados e configurados:

1. **.NET SDK 6.0** ou superior.  
   [Baixar .NET SDK](https://dotnet.microsoft.com/download)

2. **SQL Server** (versão Express ou superior).  
   [Baixar SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

3. **Visual Studio** com a carga de trabalho **ASP.NET e desenvolvimento web**.

---

### **Passo a Passo**

1. **Clone o Repositório**
   - Abra o terminal ou Git Bash.
   - Execute o comando abaixo para clonar o repositório:
     ```bash
     git clone https://github.com/seu-usuario/PopArt.git
     ```
   - Acesse a pasta do projeto:
     ```bash
     cd PopArt
     ```

2. **Configure o Banco de Dados**
   - Abra o arquivo `appsettings.json` localizado no diretório raiz do projeto.
   - Encontre a seção **ConnectionStrings** e atualize com as informações do seu SQL Server. Exemplo:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=SEU_SERVIDOR;Database=PopArtDB;User Id=SEU_USUARIO;Password=SUA_SENHA;"
     }
     ```
   - Substitua:
     - `SEU_SERVIDOR` pelo nome do seu servidor SQL.
     - `SEU_USUARIO` pelo seu usuário do SQL Server.
     - `SUA_SENHA` pela senha correspondente.

3. **Instale as Dependências**
   - No terminal, execute:
     ```bash
     dotnet restore
     ```
   - Isso irá baixar todas as dependências necessárias do projeto.

4. **Crie o Banco de Dados**
   - Certifique-se de que o SQL Server está rodando.
   - No terminal, execute:
     ```bash
     dotnet ef database update
     ```
   - Este comando aplica as migrações e cria as tabelas necessárias no banco de dados.

5. **Execute a Aplicação**
   - No terminal, inicie o servidor:
     ```bash
     dotnet run
     ```
   - Alternativamente, você pode abrir o projeto no **Visual Studio**:
     - Clique em **Executar** ou pressione **F5** para iniciar o servidor.

6. **Acesse a Aplicação**
   - Abra o navegador e vá para:
     ```
     http://localhost:5000
     ```
   - Você verá a interface inicial do **PopArt**.

---

### **Erros Comuns e Soluções**

1. **Erro de Conexão com o Banco de Dados**
   - Verifique se a string de conexão no `appsettings.json` está correta.
   - Certifique-se de que o SQL Server está rodando e acessível.

2. **Erro ao Executar Migrações**
   - Verifique se o Entity Framework Core está instalado corretamente:
     ```bash
     dotnet tool install --global dotnet-ef
     ```
   - Tente recriar as migrações:
     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

3. **Erro 404 ou Página Não Carregada**
   - Certifique-se de que o servidor foi iniciado corretamente.
   - Verifique se está acessando o endereço correto: `http://localhost:5000`.

---

### **Ferramentas e Links Úteis**

- **Download do .NET SDK**: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)  
- **Download do SQL Server**: [https://www.microsoft.com/pt-br/sql-server/sql-server-downloads](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)  
- **Documentação ASP.NET Core**: [https://docs.microsoft.com/aspnet/core](https://docs.microsoft.com/aspnet/core)  
- **Documentação Entity Framework Core**: [https://docs.microsoft.com/ef/core](https://docs.microsoft.com/ef/core)

---

Com este passo a passo, você poderá rodar o projeto **PopArt** com sucesso. Se encontrar problemas ou precisar de ajuda, entre em contato com os colaboradores do projeto.
