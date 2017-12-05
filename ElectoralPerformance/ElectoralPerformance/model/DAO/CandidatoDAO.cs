﻿using ElectoralPerformance.model.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectoralPerformance.model.DAO
{
    class CandidatoDAO
    {
        Connection connection = new Connection();
        DataTable dataTable = new DataTable();
        

        public DataTable selectCandidato(string eleicao, string codMunicipio, string idCargo)
        {
            string sql = "select 		ca.cpf, ca.nome " +
                         "from 			dados_eleicao de " +
                         "inner join 	candidato_sequencia cs on cs.sequencia = de.sequenciaCandidato " +
                         "inner join 	candidatos ca on ca.cpf = cs.cpf " +
                         "where			de.idEleicao = "+ eleicao + 
                         " and			de.codMunicipio = " + codMunicipio +
                         " and 			de.idCargo = " + idCargo;
           dataTable.Load(connection.select(sql));
            return dataTable;
        }

        
        
        public MySqlDataReader selectVotoZona()
        {
            List<CandidatoDTO> candidato = new List<CandidatoDTO>();
            CandidatoDTO candidatoDTO = new CandidatoDTO();
            string sql = "select 		ca.cpf, ca.nome, vz.zona, vz.qtdVotos " +
                         "from 			dados_eleicao de " +
                         " inner join 	candidato_sequencia cs on cs.sequencia = de.sequenciaCandidato " +
                         " inner join 	candidatos ca on ca.cpf = cs.cpf " +
                         " inner join 	votacao_zona vz on vz.sequenciaCandidato = de.sequenciaCandidato " +
                         " where			de.idEleicao = 1 " +
                         " and			de.codMunicipio = 61557 " +
                         " and 			de.idCargo = 11  ";
            MySqlDataReader dataReader = connection.select(sql);
            return dataReader;
          
        }

        public MySqlDataReader selectVotoEleicao()
        {
            string sql = "select 		ca.cpf, ca.nome, sum(vz.qtdVotos) votos " +
                            "from 			dados_eleicao de " +
                            " inner join 	candidato_sequencia cs on cs.sequencia = de.sequenciaCandidato " +
                            " inner join 	candidatos ca on ca.cpf = cs.cpf " +
                            " inner join 	votacao_zona vz on vz.sequenciaCandidato = de.sequenciaCandidato " +
                            " where			de.idEleicao = 1 " +
                            " and			de.codMunicipio = 61557 " +
                            " and 			de.idCargo = 11  " +
                            " group by ca.cpf, ca.nome order by votos desc";
            MySqlDataReader dataReader = connection.select(sql);
            return dataReader;
        }


        public DataTable selectVotoSecao()
        {
            string sql = "select c.idZona as ZONA, c.idSecao AS 'SECAO', sum(c.qtdVotos) AS VOTOS " +
                         "from voto_secao c " +
                         "where c.codMunicipio = 61557 " +
                         " and c.idCargo = 11 " +
                         " and c.numCandidato = 45 " +
                         " group by c.numCandidato, c.idZona, c.idSecao " +
                         " order by sum(c.qtdVotos) desc ";
            dataTable.Load(connection.select(sql));
            return dataTable;
        }
         

    }
}
