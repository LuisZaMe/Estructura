import React from "react";
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfHeader from "./PdfHeader";
import PdfFooter from "./PdfFooter";

const GeneralDataPage = ({study}) => {
  const testImage = "/images/image-icon.png";

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

  const data = {
    name: "Juan Pablo Coronado Rosales",
    date: "01/06/2022",
    result: "RECOMENDABLE"
  };

  return (
    <Page size={"A4"} style={{ width: '100%' }}>
      <View fixed>
        <PdfHeader />
      </View>
      <View style={[style.generalData]}>
        <View style={style.generalDataSection}>
          <Text style={[style.tittle, {marginTop: 10}]}>ESTUDIO SOCIOECÓNOMICO</Text>
          <View style={style.flexRow}>
            <Image style={style.candidateImage} src={study.candidate.media.mediaURL ?? testImage} />
            <View style={style.center}>
              <Text style={[style.label, { color: "#07DDA5", fontSize: 10 }]}>RESULTADOS DEL ESTUDIO</Text>
              <Text style={[style.label, { color: '#000', textTransform: "uppercase", fontSize: 20, fontWeight: 700 }]}>
                {study.studyStatus == 3 ? "recomendable" : "no recomendable"}
              </Text>
            </View>
          </View>
          <Text style={[style.label, { color: '#1bb9ef', marginBottom: 10 }]}>CANDIDATO</Text>
          <View style={style.flexRow}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Nombre del candidato:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.candidate.name} {study.candidate.lastname}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Fecha de solicitud:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{data.date}</Text>
            </View>
          </View>
          <View style={style.flexRow}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Puesto:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.candidate.position}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Fecha & hora de visita:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{data.date}</Text>
            </View>
          </View>
          <View style={style.flexRow}>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Empresa:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.candidate.client.companyInformation.companyName}</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableLabel}>Entrevistador:</Text>
            </View>
            <View style={[style.boxSmall, style.startCenter]}>
              <Text style={style.tableText}>{study.interviewer.name} {study.interviewer.lastname}</Text>
            </View>
          </View>
          <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10 }]}>RESUMEN FINAL</Text>
          <View style={style.flexRow}>
            <View style={[style.boxBig, style.startCenter]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Actitud en la entrevista</Text>
            </View>
            <View style={[style.boxBig, style.startCenter]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Verificación escolar</Text>
            </View>
          </View>
          <View style={style.flexRow}>
            <View style={[style.boxBig]}>
              <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.studyFinalResult.attitudeSummary}</Text>
            </View>
            <View style={[style.boxBig]}>
              <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.studyFinalResult.positionSummary}</Text>
            </View>
          </View>
          <View style={style.flexRow}>
            <View style={[style.boxBig, style.startCenter]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Trayectoria laboral</Text>
            </View>
            <View style={[style.boxBig, style.startCenter]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Junta de conciliación y arbitraje</Text>
            </View>
          </View>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig]}>
              <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.studyFinalResult.workHistorySummary}</Text>
            </View>
            <View style={[style.boxBig]}>
              <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.studyFinalResult.arbitrationAndConciliationSummary}</Text>
            </View>
          </View>
          <View style={[style.center, { padding: 10 }]}>
            <Text style={[style.label, { fontSize: 12 }]}>Atentamente</Text>
            <Text style={[style.label, { marginTop: 10, fontSize: 12 }]}>{study.studyFinalResult.finalResultsBy}</Text>
            <Text style={[style.label, { marginTop: 5, fontSize: 12 }]}>{study.studyFinalResult.finalResultsPositionBy}</Text>
          </View>
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

export default GeneralDataPage;
