import React from 'react';
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfFooter from './PdfFooter';
import PdfHeader from './PdfHeader';

const EducationPage = ({ study }) => {
  const testImage = "/images/image-icon.png";

  const lorem = `Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.`;
  const loremLong = 'Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?';

  const SchoolarLevel = {
    1: "Primaria",
    2: "Secundaria",
    3: "Bachillerato",
    4: "Licenciatura",
    5: "Posgrado"
  }

  return (
    <Page size={"A4"} style={{ width: '100%' }}>
      <View fixed>
        <PdfHeader />
      </View>
      <View style={style.generalData}>
        <View style={style.generalDataSection}>
          <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>ESCOLARIDAD</Text>
          {
            study.studySchoolarity.scholarityList.map(school => {
              return (
                <>
                  <View style={{ flexDirection: 'row' }}>
                    <View style={[style.boxBig, style.startCenter, { width: '22%' }]}>
                      <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nivel escolar</Text>
                    </View>
                    <View style={[style.boxBig, style.startCenter, { width: '22%' }]}>
                      <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nombre</Text>
                    </View>
                    <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
                      <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Periodo</Text>
                    </View>
                    <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
                      <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Años Documentos cursados</Text>
                    </View>
                    <View style={[style.boxBig, style.startCenter, { width: '14%' }]}>
                      <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Lugar</Text>
                    </View>
                    <View style={[style.boxBig, style.startCenter, { width: '14%' }]}>
                      <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Documentos</Text>
                    </View>
                  </View>
                  <View style={{ flexDirection: 'row' }}>
                    <View style={[style.boxBig, { width: '22%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{SchoolarLevel[school.schoolarLevel]}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '22%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>ESCA Tepapan del Instituto Politécnico Nacional (IPN)</Text>
                    </View>
                    <View style={[style.boxBig, { width: '12%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>2013-2017</Text>
                    </View>
                    <View style={[style.boxBig, { width: '12%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>4 años</Text>
                    </View>
                    <View style={[style.boxBig, { width: '14%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>Ciudad de México</Text>
                    </View>
                    <View style={[style.boxBig, { width: '14%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>Título y cédula profesional</Text>
                    </View>
                  </View>
                </>
              )
            })
          }
          {/* <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, style.startCenter, { width: '22%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nivel escolar</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '46%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nombre</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '28%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Carta de pasante</Text>
            </View>
          </View>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, { width: '22%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>Carrera</Text>
            </View>
            <View style={[style.boxBig, { width: '46%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>Licenciatura como Contador Público</Text>
            </View>
            <View style={[style.boxBig, { width: '28%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>####</Text>
            </View>
          </View> */}
          <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>ACTIVIDADES EXTRA CURRICULARES</Text>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, style.startCenter, { width: '20%' }]} />
            <View style={[style.boxBig, style.startCenter, { width: '20%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nombre de curso ó idioma</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '20%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nombre de Institución</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '18%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nivel</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '18%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Periodo</Text>
            </View>
          </View>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, { width: '20%' }]}>
              <Text style={[style.tableLabel, { padding: 5 }]}>Cursos recientes</Text>
            </View>
            <View style={[style.boxBig, { width: '20%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>Manejo de paquetería</Text>
            </View>
            <View style={[style.boxBig, { width: '20%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>-</Text>
            </View>
            <View style={[style.boxBig, { width: '18%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>Intermedio</Text>
            </View>
            <View style={[style.boxBig, { width: '18%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>-</Text>
            </View>
          </View>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, { width: '20%' }]}>
              <Text style={[style.tableLabel, { padding: 5 }]}>Idiomas</Text>
            </View>
            <View style={[style.boxBig, { width: '20%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>Dominio del idioma inglés</Text>
            </View>
            <View style={[style.boxBig, { width: '20%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>-</Text>
            </View>
            <View style={[style.boxBig, { width: '18%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>Intermedio</Text>
            </View>
            <View style={[style.boxBig, { width: '18%' }]}>
              <Text style={[style.tableText, { padding: 5 }]}>-</Text>
            </View>
          </View>
          <View style={[style.center, { width: '96%' }]}>
            <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>VERIFICACIÓN ESCOLAR</Text>
            <Text style={[style.tableText, { paddingVertical: 15 }]}>
              Se realizó verificación preliminar de los estudios de licenciatura de la candidata mediante consulta del
              Registro Nacional de Profesionistas de la Secretaría de Educación Pública.
            </Text>
            <Image src={study.studySchoolarity.scholarVerificationMedia.mediaURL ?? testImage} />
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

export default EducationPage