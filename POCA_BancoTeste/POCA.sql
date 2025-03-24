USE `db_POCA`;

SELECT * from tb_questoes;

INSERT INTO tb_questoes(enunciado_questao, respostacerta_questao, respostaerrada1_questao, respostaerrada2_questao, respostaerrada3_questao) VALUES
("Este é um teste?", "Sim.", "Não!", "Chute errado.", "Também não!");