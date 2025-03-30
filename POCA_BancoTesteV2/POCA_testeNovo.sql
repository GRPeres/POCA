use `db_poca`;

INSERT INTO `tb_questoes` VALUES (1,'Este é um teste?','Sim.','Não!','Chute errado.','Também não!', 'Fácil', 'Teoria');

select * from `tb_questoes`;

INSERT INTO `tb_questoes`(enunciado_questao, respostacerta_questao, respostaerrada1_questao, respostaerrada2_questao, respostaerrada3_questao, dificuldade_questao, tema_questao) VALUES ('Este é um teste?','Sim.','Não!','Chute errado.','Também não!', 'Médio', 'Programação');
