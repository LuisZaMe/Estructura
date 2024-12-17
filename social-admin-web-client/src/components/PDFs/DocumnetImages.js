import React from 'react';
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfFooter from './PdfFooter';
import PdfHeader from './PdfHeader';

const DocumnetImages = ({study}) => {
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
      name: 'Credencial INE',
      original: true,
      copy: true,
      number: "IDMEX, 000000",
      expeditionPacle: "México",
      observations: "Se cotejocon original"
    },
    {
      name: 'Comprobante de domicilio',
      original: true,
      copy: false,
      number: "IDMEX, 000000",
      expeditionPacle: "México",
      observations: "Se cotejocon original"
    },
    {
      name: 'Acta de nacimiento',
      original: true,
      copy: true,
      number: "xxx",
      expeditionPacle: "México",
      observations: "Se cotejocon original"
    },
    {
      name: 'CURP',
      original: true,
      copy: true,
      number: "XXXX0000XXXXX",
      expeditionPacle: "México",
      observations: "Se cotejocon original"
    },
    {
      name: 'Comprobante de estudios',
      original: true,
      copy: true,
      number: "###",
      expeditionPacle: "Ciudad de México",
      observations: "Se cotejocon original"
    },
    {
      name: 'Número de IMSS',
      original: true,
      copy: true,
      number: "###",
      expeditionPacle: "Esado de México",
      observations: "Se cotejocon original"
    },
  ]

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
      <View style={style.generalData}>
        <View style={style.generalDataSection}>
          <View style={[style.flexRow, { width: '90%', padding: 15, marginVertical: 15 }]}>
            <View style={[style.center, { width: '50%', marginRight: 20 }]}>
              <Text style={[style.label, { color: '#1bb9ef' }]}>CREDENCIAL INE FRENTE</Text>
              <Image style={{width: '80%'}} src={study.studyGeneralInformation.ineFrontMedia.mediaURL ?? testImage} />
            </View>
            <View style={[style.center, { width: '50%' }]}>
              <Text style={[style.label, { color: '#1bb9ef' }]}>CREDENCIAL INE VUELTA</Text>
              <Image style={{width: '80%'}} src={study.studyGeneralInformation.ineBackMedia.mediaURL ?? testImage} />
            </View>
          </View>
          <View style={[style.flexRow, { width: '90%', padding: 15, marginBottom: 15 }]}>
            <View style={[style.center, { width: '50%', marginRight: 20 }]}>
              <Text style={[style.label, { color: '#1bb9ef' }]}>COMPROBANTE DE DOMICILIO</Text>
              <Image style={{width: '80%'}} src={study.studyGeneralInformation.addressProofMedia.mediaURL ?? testImage} />
            </View>
            <View style={[style.center, { width: '50%' }]}>
              <Text style={[style.label, { color: '#1bb9ef' }]}>ACTA DE NACIMIENTO</Text>
              <Image style={{width: '80%'}} src={study.studyGeneralInformation.bornCertificateMedia.mediaURL ?? testImage} />
            </View>
          </View>
          <View style={[style.flexRow, { width: '90%', padding: 15, marginBottom: 15 }]}>
            <View style={[style.center, { width: '50%', marginRight: 20 }]}>
              <Text style={[style.label, { color: '#1bb9ef' }]}>CURP</Text>
              <Image style={{width: '80%'}} src={study.studyGeneralInformation.curpMedia.mediaURL ?? testImage} />
            </View>
            <View style={[style.center, { width: '50%' }]}>
              <Text style={[style.label, { color: '#1bb9ef' }]}>COMPROBANTE DE ESTUDIOS</Text>
              <Image style={{width: '80%'}} src={study.studyGeneralInformation.studiesProofMedia.mediaURL ?? testImage} />
            </View>
          </View>
          <View style={{ width: '90%', padding: 15 }}>
            <View style={[style.center]}>
              <Text style={[style.label, { color: '#1bb9ef' }]}>NÚMERO DE IMSS</Text>
              <Image style={{width: '80%'}} src={study.studyGeneralInformation.socialSecurityProofMedia.mediaURL ?? testImage} />
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

export default DocumnetImages