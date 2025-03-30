# Exercicio 1 - Quebra-Cabeça dos Números Escondidos  

## Descrição  
O objetivo deste projeto é encontrar todos os números inteiros positivos dentro de um intervalo especificado que atendam às seguintes condições:  

1. **Divisibilidade**: O número deve ser divisível por um valor específico.  
2. **Soma dos Dígitos**: A soma dos dígitos do número deve ser maior ou igual a um valor mínimo.  
3. **Dígitos Proibidos**: O número não pode conter determinados dígitos proibidos.  

## Tecnologias Utilizadas  
- **.NET 6**  

## Estrutura do Projeto  
O projeto contém:  
- **Program.cs**: Implementação da lógica principal.  
- **Função `EncontrarNumerosEscondidos`**: Processa os números dentro do intervalo e filtra de acordo com as regras.  
- **Interação com o usuário**: Permite inserir valores manualmente ou rodar um teste pronto.  

## Configuração e Execução  
### 1. Clonar o repositório  
```sh  
git clone https://github.com/KarollineLUP-dev/avaliacao-tecnica.git  
cd quebra-cabeca  
dotnet run  
```

### 2. Uso  
Ao executar o programa, o usuário pode escolher entre:  
- Inserir valores manualmente.  
- Rodar um teste pronto com valores predefinidos.  

### 3. Exemplo de Entrada e Saída  

#### Entrada  
```
Digite o valor de A: 1  
Digite o valor de B: 75  
Digite o valor de C: 3  
Digite o valor de D: 2  
Digite os valores proibidos separando-os por vírgula: 1,5,8  
```

#### Saída  
```
Valores utilizados: A=1, B=75, C=3, D=2, E=[1, 5, 8]  
Resultado: [3, 6, 9, 12, 18, 21, 24, 27, 30, 33, 36, 39, 42, 45, 48, 51, 54, 57, 60, 63, 69, 72]  
```  
_____________________________________________________

# Exercicio 2 - API de Produtos

## Descrição
Esta é uma API para gerenciamento de produtos, permitindo listar, buscar, criar, atualizar e deletar produtos.

## Tecnologias Utilizadas
- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **Swagger para documentação**

## Estrutura do Projeto
A Solution contém os seguintes projetos:
- **ApiProduto**: Projeto principal contendo os controllers e configuração do Entity Framework.
- **ApiProduto.Data**: Responsável pelo contexto do banco de dados e a configuração do EF.

## Configuração e Execução
### 1. Clonar o repositório
```sh
 git clone https://github.com/KarollineLUP-dev/avaliacao-tecnica.git
 cd api-produto
```
### 2. Configurar o Banco de Dados

A API utiliza o **Entity Framework Core** e suporta **Code-First**.
Por padrão, usa-se um banco de dados em memória para testes, mas também é possível configurar o **SQL Server** no `appsettings.json`.

#### **Usando banco de dados em memória:**
A API já está configurada para rodar usando um banco em memória. Basta iniciar a aplicação.

#### **Usando SQL Server:**
1. Configure a string de conexão no `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=ProdutosDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
2. Execute as migrações para criar o banco de dados:
```sh
dotnet ef database update
```

### 3. Executar a API
```sh
dotnet run
```
A API estará disponível em `http://localhost:5119/swagger`.

## Endpoints
### **Listar Produtos**
```http
GET /api/produtos/listar?name={name}
```
Retorna uma a lista de produtos com nomes semelhantes ao informado.

### **Listar Produtos Ordenados**
```http
GET /api/produtos/ordenar?orderBy={orderBy}
```
Retorna uma a lista de produtos ordenados pelo campo em específico.

### **Buscar Produto por ID**
```http
GET /api/produtos/consultar/{id}
```
Retorna um produto específico pelo identificador.

### **Buscar Produto por Nome**
```http
GET /api/produtos?name={name}
```
Retorna um produto específico pelo nome.

### **Criar Produto**
```http
POST /api/produtos
Content-Type: application/json
{
  "nome": "Notebook Dell",
  "estoque": 10,
  "valor": 4500.00
}
```

### **Atualizar Produto**
```http
PUT /api/produtos/{id}
Content-Type: application/json
{
  "nome": "Mouse Multilaser",
  "estoque": 73,
  "valor": 35.00
}
```

### **Deletar Produto**
```http
DELETE /api/produtos/{id}
```

## População Inicial da Base de Dados
Ao iniciar a aplicação, a base será populada automaticamente com 5 produtos:

| ID  | Nome               | Estoque | Valor  |
|-----|--------------------|---------|--------|
| 1   | Notebook Asus      |   23    | 3800.00|
| 2   | Mouse   Logitech   |   51    | 150.00 |
| 3   | Teclado Logitech   |   19    | 380.00 |
| 4   | Monitor AOC        |   15    | 560.00 |
| 5   | Impressora Canon   |   3     | 890.00 |


## Autor
**Karolline Lopes Urtado Pereira** 



