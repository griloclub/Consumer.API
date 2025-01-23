# Consumer

API em .NET 8 para sistema de gestão de mesas e comandas de restaurante.

## Visão Geral

API REST construída com .NET 8 para gerenciamento de mesas, comandas e autenticação de usuários.

## Funcionalidades

# O sistema fornece endpoints para gerenciar mesas, pedidos e acesso de usuários.
- 
- Autenticação baseada em JWT
- Gerenciamento de mesas/comandas
- Informações detalhadas dos pedidos

## Endpoints da API

### Autenticação
- POST /api/auth/login - Autenticação de usuário

### Mesas
- GET /api/tables - Listar mesas/comandas abertas
- GET /api/tables/{id} - Obter informações detalhadas da mesa
