import React, { useState } from 'react';
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfFooter from './PdfFooter';
import PdfHeader from './PdfHeader';
import moment from 'moment';
import 'moment/locale/es';

const OficialDocumentationPage = ({ study }) => {

  
  const lorem = `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.`;
  const loremLong = 'Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?';
  
  const maritalStatus = {
    1: "Soltero(a)",
    2: "Casado(a)",
    3: "Vieworciado(a)",
    4: "Separación en proceso judicial",
    5: "Viudo(a)",
    6: "Concubinato"
  }

  const bornDate = moment(study.studyGeneralInformation.bornDate);
  bornDate.locale('es');

  let documents = [
    {
      name: 'Credencial INE',
      original: study.studyGeneralInformation.idCardOriginal,
      copy: study.studyGeneralInformation.idCardCopy,
      number: study.studyGeneralInformation.idCardRecord,
      expeditionPacle: study.studyGeneralInformation.idCardExpeditionPlace,
      observations: study.studyGeneralInformation.idCardObservations
    },
    {
      name: 'Comprobante de domicilio',
      original: study.studyGeneralInformation.addressProofOriginal,
      copy: study.studyGeneralInformation.addressProofCopy,
      number: study.studyGeneralInformation.addressProofRecord,
      expeditionPacle: study.studyGeneralInformation.addressProofExpeditionPlace,
      observations: study.studyGeneralInformation.addressProofObservations
    },
    {
      name: 'Acta de nacimiento',
      original: study.studyGeneralInformation.birthCertificateOriginal,
      copy: study.studyGeneralInformation.birthCertificateCopy,
      number: study.studyGeneralInformation.birthCertificateRecord,
      expeditionPacle: study.studyGeneralInformation.birthCertificateExpeditionPlace,
      observations: study.studyGeneralInformation.birthCertificateObservations
    },
    {
      name: 'CURP',
      original: study.studyGeneralInformation.curpOriginal,
      copy: study.studyGeneralInformation.curpCopy,
      number: study.studyGeneralInformation.curpRecord,
      expeditionPacle: study.studyGeneralInformation.curpExpeditionPlace,
      observations: study.studyGeneralInformation.curpObservations
    },
    {
      name: 'Comprobante de estudios',
      original: study.studyGeneralInformation.studyProofOriginal,
      copy: study.studyGeneralInformation.studyProofCopy,
      number: study.studyGeneralInformation.studyProofRecord,
      expeditionPacle: study.studyGeneralInformation.studyProofExpeditionPlace,
      observations: study.studyGeneralInformation.studyProofObservations
    },
    {
      name: 'Número de IMSS',
      original: study.studyGeneralInformation.socialSecurityProofOriginal,
      copy: study.studyGeneralInformation.socialSecurityProofCopy,
      number: study.studyGeneralInformation.socialSecurityProofRecord,
      expeditionPacle: study.studyGeneralInformation.socialSecurityProofExpeditionPlace,
      observations: study.studyGeneralInformation.socialSecurityProofObservations
    },
    {
      name: 'Cartilla Militar',
      original: study.studyGeneralInformation.militaryLetterOriginal,
      copy: study.studyGeneralInformation.militaryLetterCopy,
      number: study.studyGeneralInformation.militaryLetterRecord,
      expeditionPacle: study.studyGeneralInformation.militaryLetterExpeditionPlace,
      observations: study.studyGeneralInformation.militaryLetterObservations
    },
    {
      name: 'RFC',
      original: study.studyGeneralInformation.rfcOriginal,
      copy: study.studyGeneralInformation.rfcCopy,
      number: study.studyGeneralInformation.rfcRecord,
      expeditionPacle: study.studyGeneralInformation.rfcExpeditionPlace,
      observations: study.studyGeneralInformation.rfcObservations
    },
    {
      name: 'Carta de Antecedentes Penales',
      original: study.studyGeneralInformation.criminalRecordLetterOriginal,
      copy: study.studyGeneralInformation.criminalRecordLetterCopy,
      number: study.studyGeneralInformation.criminalRecordLetterRecord,
      expeditionPacle: study.studyGeneralInformation.criminalRecordLetterExpeditionPlace,
      observations: study.studyGeneralInformation.criminalRecordLetterObservations
    },
  ]

  return (
    <Page size={"A4"} style={{ width: '100%' }}>
      <View fixed>
        <PdfHeader />
      </View>
      <View style={style.generalData}>
        <View style={style.generalDataSection}>
          {study.workStudy != null ? <Text style={[style.tittle, { marginTop: 10 }]}>ESTUDIO SOCIOLABORAL</Text> : null}
          <Text style={[style.label, { color: '#07DDA5', marginVertical: 10, }]}>{study.workStudy != null ? "CANDIDATO" : "A. CANDIDATO"}</Text>
          {study.workStudy != null ? <Text style={[style.label, { color: '#07DDA5', marginVertical: 10, }]}>Datos Generales</Text> : null}
          {
            study.workStudy != null ?
              <View style={style.generalDataSection}>
                <View style={{ flexDirection: "row" }}>
                  <View style={[style.boxBig, { width: '26%' }]}>
                    <Text style={[style.tableLabel, { padding: 5 }]}>Nombre del candidato:</Text>
                  </View>
                  <View style={[style.boxBig, { width: '70%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{study.studyGeneralInformation.name}</Text>
                  </View>
                </View>
                <View style={{ flexDirection: "row" }}>
                  <View style={[style.boxBig, { width: '26%' }]}>
                    <Text style={[style.tableLabel, { padding: 5 }]}>Empresa solicitante:</Text>
                  </View>
                  <View style={[style.boxBig, { width: '70%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{study.candidate.client.companyInformation.companyName}</Text>
                  </View>
                </View>
                <View style={{ flexDirection: "row" }}>
                  <View style={[style.boxBig, { width: '26%' }]}>
                    <Text style={[style.tableLabel, { padding: 5 }]}>Estatus:</Text>
                  </View>
                  <View style={[style.boxBig, { width: '70%', textTransform: "uppercase", color: '#07DDA5' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>
                      {study.studyStatus == 3 ? "recomendable" : "no recomendable"}
                    </Text>
                  </View>
                </View>
              </View>
              : null
          }
          <View style={{ flexDirection: "row", marginTop: 10 }}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Nombre:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.name}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Núm. Empleado:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.employeeNumber ?? "No aplica"}</Text>
            </View>
          </View>
          <View style={{ flexDirection: "row" }}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Antigüedad en la institución:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.timeOnComany ?? "No aplica"}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Nacionalidad:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.countryName}</Text>
            </View>
          </View>
          <View style={{ flexDirection: "row" }}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Edad:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.age}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Lugar & Fecha de nacimiento:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.bornState.name}, {bornDate.format("LL")}</Text>
            </View>
          </View>
          <View style={{ flexDirection: "row" }}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Regimen:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.taxRegime}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Estado civil:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{maritalStatus[study.studyGeneralInformation.maritalStatus]}</Text>
            </View>
          </View>
          <View style={{ flexDirection: "row" }}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Codigo Postal:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.postalCode}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Domicilio:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.address}</Text>
            </View>
          </View>
          <View style={{ flexDirection: "row" }}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Colonia:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.suburb}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Delegación:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.delegation ?? ""}</Text>
            </View>
          </View>
          <View style={{ flexDirection: "row" }}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Teléfono casa:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.homePhone}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Teléfono celular:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.mobilPhone}</Text>
            </View>
          </View>
          <View style={{ flexDirection: "row" }}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Correo electrónico:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.studyGeneralInformation.email}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]} />
            <View style={[style.boxSmall, style.startCenter]} />
          </View>
          <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10 }]}>DOCUMENTACIÓN OFICIAL</Text>
          <View style={{ flexDirection: "row" }}>
            <View style={[style.boxBig, style.startCenter, { width: '22%', height: 45 }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Documento</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%', height: 45 }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Original</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%', height: 45 }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Copia</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '22%', height: 45 }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>No. de acta o folio</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '14%', height: 45 }]}>
              <View style={style.center}>
                <Text style={[style.tableLabel, { paddingTop: 5 }]}>Lugar de</Text>
                <Text style={[style.tableLabel, { paddingBottom: 5 }]}>expedición</Text>
              </View>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '14%', height: 45 }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Observaciones</Text>
            </View>
          </View>
          {documents.map(document => {

            return (
              <View style={{ flexDirection: 'row' }}>
                <View style={[style.boxBig, { width: '22%' }]}>
                  <Text style={[style.tableText, { padding: 5 }]}>{document.name}</Text>
                </View>
                <View style={[style.boxBig, style.center, { width: '12%', padding: 0 }]} >
                  {document.original ? <Image style={{width: 15, height: 15}} src={"/images/Icon_check.png"} /> : null}
                </View>
                <View style={[style.boxBig, style.center, { width: '12%', padding: 0 }]} >
                  {document.copy ? <Image style={{width: 15, height: 15}} src={"/images/Icon_check.png"} /> : null}
                </View>
                <View style={[style.boxBig, { width: '22%' }]}>
                  <Text style={[style.tableText, { padding: 5 }]}>{document.number}</Text>
                </View>
                <View style={[style.boxBig, { width: '14%' }]}>
                  <Text style={[style.tableText, { padding: 5 }]}>{document.expeditionPacle}</Text>
                </View>
                <View style={[style.boxBig, { width: '14%' }]}>
                  <Text style={[style.tableText, { padding: 5 }]}>{document.observations}</Text>
                </View>
              </View>)
          })}
        </View>
      </View>
      <View fixed style={[style.center, { position: "absolute", bottom: 0, width: '100%' }]}>
        <View style={[style.center, { width: '100%' }]}>
          <PdfFooter />
        </View>
      </View>
    </Page>
  )
}

export default OficialDocumentationPage;