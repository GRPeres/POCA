-- 1. Adicionando 5 novas pessoas (tb_pessoas)
INSERT INTO tb_pessoas (login_pessoa, senha_pessoa, bool_professor_pessoa) VALUES
('felipe', 'senha111', 0),
('gabriela', 'senha222', 0),
('heitor', 'senha333', 0),
('isabela', 'senha444', 1),
('joao', 'senha555', 1);

-- 2. Adicionando 3 novos alunos (tb_alunos)
INSERT INTO tb_alunos (nome_aluno, nascimento_aluno, progresso_aluno, contato_aluno) VALUES
('Felipe Costa', '2001-05-10', 30, 'felipe@email.com'),
('Gabriela Martins', '2000-07-15', 50, 'gabriela@email.com'),
('Heitor Oliveira', '1999-08-20', 80, 'heitor@email.com');

-- 3. Relacionando novas pessoas com alunos (tb_pessoas_has_tb_alunos)
INSERT INTO tb_pessoas_has_tb_alunos (tb_pessoas_id_pessoa, tb_alunos_id_aluno) VALUES
(6, 4),
(7, 5),
(8, 6);

-- 4. Adicionando 2 novos professores (tb_professores)
INSERT INTO tb_professores (nome_professor, materia_professor, contato_professor) VALUES
('Isabela Santos', 'História da Computação', 'isabela@email.com'),
('João Pereira', 'Inglês Técnico', 'joao@email.com');

-- 5. Relacionando novas pessoas com professores (tb_professores_has_tb_pessoas)
INSERT INTO tb_professores_has_tb_pessoas (tb_professores_id_professor, tb_pessoas_id_pessoa) VALUES
(3, 9),
(4, 10);

-- 6. Adicionando 1 nova matéria (tb_materias)
INSERT INTO tb_materias (nome_materia) VALUES
('História da Computação');

-- 7. Relacionando novos professores com matérias (tb_professores_has_tb_materias)
INSERT INTO tb_professores_has_tb_materias (tb_professores_id_professor, tb_materias_id_materia) VALUES
(3, 4),
(4, 4);  -- Assumindo que Inglês Técnico usa a mesma matéria

-- 8. Relacionando novos alunos com matérias (tb_alunos_has_tb_materias)
INSERT INTO tb_alunos_has_tb_materias (tb_alunos_id_aluno, tb_materias_id_materia) VALUES
(4, 4),
(5, 1),
(6, 3);

-- 9. Adicionando 3 novas atividades (tb_atividades)
INSERT INTO tb_atividades (nome_atividade) VALUES
('Atividade 4'),
('Atividade 5'),
('Atividade 6');

-- 10. Relacionando novas atividades com matérias (tb_atividades_has_tb_materias)
INSERT INTO tb_atividades_has_tb_materias (tb_atividades_id_atividade, tb_materias_id_materia) VALUES
(4, 4),
(5, 2),
(6, 3);

-- 11. Adicionando 20 NOVAS QUESTÕES (tb_questoes)
INSERT INTO tb_questoes (enunciado_questao, respostacerta_questao, respostaerrada1_questao, respostaerrada2_questao, respostaerrada3_questao, dificuldade_questao, tema_questao) VALUES
-- Questões 4-23
('Qual comando SQL é usado para inserir dados?', 'INSERT', 'CREATE', 'DELETE', 'UPDATE', 'Fácil', 'SQL'),
('Em POO, o que é encapsulamento?', 'Proteção de dados internos', 'Herança de características', 'Polimorfismo de métodos', 'Abstração de classes', 'Médio', 'Programação'),
('Qual função converte string para int em Python?', 'int()', 'str()', 'float()', 'input()', 'Fácil', 'Python'),
('O que é um JOIN em SQL?', 'Combinar tabelas', 'Ordenar resultados', 'Filtrar registros', 'Agrupar dados', 'Médio', 'SQL'),
('Qual é o operador lógico "OU" em Python?', 'or', 'and', 'not', 'xor', 'Fácil', 'Python'),
('Em qual ano foi criado o Python?', '1991', '1989', '1995', '2000', 'Difícil', 'História'),
('O que significa HTTP?', 'HyperText Transfer Protocol', 'High Transfer Text Protocol', 'Hyper Transfer Text Protocol', 'High Text Transfer Protocol', 'Médio', 'Redes'),
('Qual não é um tipo de dado em SQL?', 'loop', 'varchar', 'int', 'date', 'Fácil', 'SQL'),
('O que é um DataFrame?', 'Estrutura de dados tabular', 'Tipo de gráfico', 'Função matemática', 'Protocolo de rede', 'Médio', 'Dados'),
('Qual linguagem é usada para estilizar páginas web?', 'CSS', 'HTML', 'JavaScript', 'Python', 'Fácil', 'Web'),
('O que o comando git commit faz?', 'Salva alterações localmente', 'Envia para o repositório', 'Clona um repositório', 'Cria um novo branch', 'Médio', 'DevOps'),
('Qual é a complexidade do algoritmo Bubble Sort?', 'O(n²)', 'O(n log n)', 'O(n)', 'O(log n)', 'Difícil', 'Algoritmos'),
('O que é um NFT?', 'Token não-fungível', 'Protocolo de rede', 'Criptomoeda', 'Framework web', 'Médio', 'Blockchain'),
('Qual função retorna o tamanho de uma lista?', 'len()', 'size()', 'count()', 'length()', 'Fácil', 'Python'),
('O que é uma chave estrangeira?', 'Referência a outra tabela', 'Chave primária duplicada', 'Tipo de criptografia', 'Restrição de acesso', 'Médio', 'SQL'),
('Qual comando inicia um servidor Node.js?', 'node app.js', 'npm start', 'node start', 'npm run', 'Médio', 'JavaScript'),
('O que é um atributo em UML?', 'Característica de uma classe', 'Tipo de relacionamento', 'Comportamento de objeto', 'Diagrama de sequência', 'Difícil', 'Engenharia de Software'),
('Qual método adiciona item no final de uma lista?', 'append()', 'insert()', 'add()', 'push()', 'Fácil', 'Python'),
('O que é Big O Notation?', 'Medida de complexidade', 'Tipo de algoritmo', 'Sistema numérico', 'Padrão de projeto', 'Difícil', 'Algoritmos'),
('Qual protocolo é usado para e-mails?', 'SMTP', 'HTTP', 'FTP', 'TCP', 'Médio', 'Redes');

-- 12. Relacionando novas questões com atividades (tb_questoes_has_tb_atividades)
-- Distribuindo as 20 questões entre as 6 atividades existentes
INSERT INTO tb_questoes_has_tb_atividades (tb_questoes_id_questao, tb_atividades_id_atividade) VALUES
(4, 1), (5, 1), (6, 1), (7, 1),   -- Atividade 1 (4 questões)
(8, 2), (9, 2), (10, 2), (11, 2), -- Atividade 2 (4 questões)
(12, 3), (13, 3), (14, 3), (15, 3), -- Atividade 3 (4 questões)
(16, 4), (17, 4), (18, 4),        -- Atividade 4 (3 questões)
(19, 5), (20, 5), (21, 5),        -- Atividade 5 (3 questões)
(22, 6), (23, 6);                 -- Atividade 6 (2 questões)