import React from 'react';
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfFooter from './PdfFooter';
import PdfHeader from './PdfHeader';

const PersonalReferencesPage = ({study, index}) => {
  const testImage = "/images/image-icon.png";

  return (
    <>
      <Page size={"A4"} style={{ width: '100%' }}>
        <View fixed>
          <PdfHeader />
        </View>
        <View style={style.generalData}>
          <View style={style.generalDataSection}>
            {index == 0 ? <Text style={[style.label, { color: '#07DDA5', marginVertical: 10, }]}>G. REFERENCIA PERSONAL</Text> : null}
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig, style.startCenter]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nombre</Text>
              </View>
              <View style={[style.boxBig, style.startCenter]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.name}</Text>
              </View>
            </View>
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Ocupación actual</Text>
              </View>
              <View style={[style.boxBig]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.currentJob}</Text>
              </View>
            </View>
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Dirección</Text>
              </View>
              <View style={[style.boxBig]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.address}</Text>
              </View>
            </View>
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Teléfono</Text>
              </View>
              <View style={[style.boxBig]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.phone}</Text>
              </View>
            </View>
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>¿Cuánto tiempo tiene de conocer a nuestra investigada?</Text>
              </View>
              <View style={[style.boxBig]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.yearsKnowingEachOther}</Text>
              </View>
            </View>
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>¿Sabe ud. en dóndé vive nuestra investigada?</Text>
              </View>
              <View style={[style.boxBig]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.knowAddress}</Text>
              </View>
            </View>
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>¿Sabe ud. cuánto tiempo tiene radicando en su domicilio actual?</Text>
              </View>
              <View style={[style.boxBig]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.yearsOnCurrentResidence}</Text>
              </View>
            </View>
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>¿Sabe donde laborado?</Text>
              </View>
              <View style={[style.boxBig]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.knowledgeAboutPreviousJobs}</Text>
              </View>
            </View>
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>¿Qué opinión tiene deuestra investigada?</Text>
              </View>
              <View style={[style.boxBig]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.opinionAboutTheCandidate}</Text>
              </View>
            </View>
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>¿Sabe ud. si nuestra investigada ha participado en alguna actividad política?</Text>
              </View>
              <View style={[style.boxBig]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.politicalActivity}</Text>
              </View>
            </View>
            <View style={{flexDirection: 'row'}}>
              <View style={[style.boxBig]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>¿Ud. la recomienda?</Text>
              </View>
              <View style={[style.boxBig]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.wouldYouRecommendIt}</Text>
              </View>
            </View>
          </View>
        </View>
        <View fixed style={[style.center, { position: "absolute", bottom: 0, width: '100%', display: 'block' }]}>
          <View style={[style.center, { width: '100%' }]}>
            <PdfFooter />
          </View>
        </View>
      </Page>
      <Page size={"A4"} style={{ width: '100%' }}>
        <View fixed>
          <PdfHeader />
        </View>
        <View style={style.generalData}>
          <View style={style.generalDataSection}>
            <View style={[style.flexRow, { width: '90%', padding: 15, marginVertical: 15 }]}>
              <View style={[style.center, { width: '50%', marginRight: 20 }]}>
                <Image src={testImage} />
              </View>
              <View style={[style.center, { width: '50%' }]}>
                <Image src={testImage} />
              </View>
            </View>
            <View style={[style.flexRow, { width: '90%', padding: 15, marginBottom: 15 }]}>
              <View style={[style.center, { width: '50%', marginRight: 20 }]}>
                <Image src={testImage} />
              </View>
              <View style={[style.center, { width: '50%' }]}>
                <Image src={testImage} />
              </View>
            </View>
            <View style={[style.flexRow, { width: '90%', padding: 15, marginBottom: 15 }]}>
              <View style={[style.center, { width: '50%', marginRight: 20 }]}>
                <Image src={testImage} />
              </View>
              <View style={[style.center, { width: '50%' }]}>
                <Image src={testImage} />
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
    </>
  )
}

export default PersonalReferencesPage