import React from 'react';
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfFooter from './PdfFooter';
import PdfHeader from './PdfHeader';
import moment from 'moment';

const ImssValidationPage = ({ study }) => {
  const creditGrantDate = study.studyIMSSValidation.grantDate != "" ? moment(study.studyIMSSValidation.grantDate) : ""
  return (
    <Page size={"A4"} style={{ width: '100%' }}>
      <View fixed>
        <PdfHeader />
      </View>
      <View style={style.generalData}>
        <View style={style.generalDataSection}>
          <Text style={[style.label, { color: '#07DDA5', marginVertical: 10, }]}>F. VALIDACIÓN IMSS</Text>
          <Text style={[style.label, { marginVertical: 10, }]}>VALIDACIÓN IMSS</Text>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, style.startCenter, { width: '42%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Razón social</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '18%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Fecha de ingreso</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '18%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Fecha de baja</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '18%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Resultado</Text>
            </View>
          </View>

          {
            study.studyIMSSValidation.imssValidationList.map(imms => {
              const startDate = moment(imms.startDate);
              const endDate = moment(imms.endDate);
              return (
                <View style={{ flexDirection: 'row' }}>
                  <View style={[style.boxBig, style.startCenter, { width: '42%' }]}>
                    <Text style={[style.tableText, { paddingVertical: 5 }]}>{imms.companyBusinessName}</Text>
                  </View>
                  <View style={[style.boxBig, style.startCenter, { width: '18%' }]}>
                    <Text style={[style.tableText, { paddingVertical: 5 }]}>{startDate.format("DD/MM/YYYY")}</Text>
                  </View>
                  <View style={[style.boxBig, style.startCenter, { width: '18%' }]}>
                    <Text style={[style.tableText, { paddingVertical: 5 }]}>{endDate.format("DD/MM/YYYY")}</Text>
                  </View>
                  <View style={[style.boxBig, style.startCenter, { width: '18%' }]}>
                    <Text style={[style.tableText, { paddingVertical: 5 }]}>{imms.result}</Text>
                  </View>
                </View>
              )
            })
          }

          <Text style={[style.label, { marginVertical: 10, }]}>Validación INFONAVIT</Text>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, style.startCenter, { width: '32%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Número de crédito</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '32%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Estatus</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '32%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Fecha de otorgamiento</Text>
            </View>
          </View>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, { width: '32%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>{study.studyIMSSValidation.creditNumber}</Text>
            </View>
            <View style={[style.boxBig, { width: '32%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>{study.studyIMSSValidation.creditStatus}</Text>
            </View>
            <View style={[style.boxBig, { width: '32%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>{creditGrantDate == "" ? creditGrantDate : creditGrantDate.format("DD/MM/YYYY")}</Text>
            </View>
          </View>
          <View style={[style.center, { width: '96%' }]}>
            <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>VERIFICACIÓN POR DEMANDAS ANTE LA JUNTA DE CONCILILACIÓN Y ARBITRAJE</Text>
            <Text style={[style.tableText, { paddingVertical: 15 }]}>{study.studyIMSSValidation.conciliationClaimsSummary}</Text>
          </View>
        </View>
      </View>
      <View fixed style={[style.center, { position: "absolute", bottom: 0, width: '100%', display: 'block' }]}>
        <View style={[style.center, { width: '100%' }]}>
          <PdfFooter />
        </View>
      </View>
    </Page>
  )
}

export default ImssValidationPage