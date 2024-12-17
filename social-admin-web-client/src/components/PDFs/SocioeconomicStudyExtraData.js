import React from 'react';
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfFooter from './PdfFooter';
import PdfHeader from './PdfHeader';
import moment from 'moment';

const SocioeconomicStudyExtraData = ({study}) => {
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

  const documents = [
    {
      name: 'Doall Consultoría y Servicios, S.A. de C.V.',
      number: "01/02/2019",
      expeditionPacle: "01/02/2020",
      observations: "Se cotejocon original"
    },
    {
      name: 'MOBEL',
      number: "04/02/2020",
      expeditionPacle: "01/02/2021",
      observations: "Se cotejocon original"
    },
  ]

  return (
    <Page size={"A4"} style={{ width: '100%' }}>
      <View fixed>
        <PdfHeader />
      </View>
      <View style={style.generalData}>
        <View style={style.generalDataSection}>
          <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>CARTAS DE RECOMENDACIÓN Y/O CONSTANCIAS LABORALES</Text>
          <View style={style.flexRow}>
            <View style={[style.boxBig, style.startCenter, { width: '34%', height: 45 }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Empresa que expide</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '14%', height: 45 }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>De</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '14%', height: 45 }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>A</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '34%', height: 45 }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Años laborados</Text>
            </View>
          </View>
          {study.studyGeneralInformation.recommendationCards.map(document => {
            const from = moment(document.workingFrom);
            const to = moment(document.workingTo);
            return (
              <View key={document.id} style={{ flexDirection: 'row' }}>
                <View style={[style.boxBig, { width: '34%' }]}>
                  <Text style={[style.tableText, { padding: 5 }]}>{document.issueCompany}</Text>
                </View>
                <View style={[style.boxBig, { width: '14%' }]}>
                  <Text style={[style.tableText, { padding: 5 }]}>{from.format("DD/MM/YYYY")}</Text>
                </View>
                <View style={[style.boxBig, { width: '14%' }]}>
                  <Text style={[style.tableText, { padding: 5 }]}>{to.format("DD/MM/YYYY")}</Text>
                </View>
                <View style={[style.boxBig, { width: '34%' }]}>
                  <Text style={[style.tableText, { padding: 5 }]}>{document.timeWorking}</Text>
                </View>
              </View>)
          })}

          <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10 }]}>VERIFICACIÓN IDENTIFICACIÓN OFICIAL INE</Text>
          <View style={[style.flexRow, { width: '90%', padding: 15, marginVertical: 15 }]}>
            <View style={[style.center, { width: '50%', marginRight: 20 }]}>
              <Text style={[style.label, { color: '#1bb9ef' }]}>CREDENCIAL INE FRENTE</Text>
              <Image src={study.studyGeneralInformation.ineFrontMedia.mediaURL ?? testImage} />
            </View>
            <View style={[style.center, { width: '50%' }]}>
              <Text style={[style.label, { color: '#1bb9ef' }]}>CREDENCIAL INE VUELTA</Text>
              <Image src={study.studyGeneralInformation.ineBackMedia.mediaURL ?? testImage} />
            </View>
          </View>
          <View style={{ width: '90%', padding: 15 }}>
            <View style={[style.center]}>
              <Text style={[style.label, { color: '#1bb9ef' }]}>VERIFICACIÓN</Text>
              <Image src={study.studyGeneralInformation.verificationMedia.mediaURL ?? testImage} />
            </View>
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

export default SocioeconomicStudyExtraData