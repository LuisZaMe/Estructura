import React from 'react';
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfFooter from './PdfFooter';
import PdfHeader from './PdfHeader';

const SocialPage = ({ study }) => {
  return (
    <Page size={"A4"} style={{ width: '100%' }}>
      <View fixed>
        <PdfHeader />
      </View>
      <View style={style.generalData}>
        <View style={style.generalDataSection}>
          <Text style={[style.label, { color: '#07DDA5', marginVertical: 10, }]}>D. SOCIAL</Text>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, style.startCenter, { width: '32%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Los valores que le inculcaron sus padres y que trata de llevar a cabo en la vida diaria son</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '32%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Su prioridad en la vida</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '32%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Meta más próxima</Text>
            </View>
          </View>
          {
            study.studySocial.socialGoalsList.map(goal => {
              return (
                <View style={{ flexDirection: 'row' }}>
                  <View style={[style.boxBig, { width: '32%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{goal.coreValue}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '32%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{goal.lifeGoal}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '32%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{goal.nextGoal}</Text>
                  </View>
                </View>
              )
            })
          }
          <View style={{ width: '96%', flexDirection: 'row', marginTop: 20 }}>
            <View style={{ width: '15%' }}>
              <Text style={[style.tableText, { color: '#07DDA5', paddingRight: 5 }]}>
                Área Social:
              </Text>
            </View>
            <View style={{ width: '85%' }}>
              <Text style={[style.tableText]}>{study.studySocial.socialArea}</Text>
            </View>
          </View>
          <View style={{ width: '96%', flexDirection: 'row', marginTop: 20 }}>
            <View style={{ width: '15%' }}>
              <Text style={[style.tableText, { color: '#07DDA5', paddingRight: 5 }]}>
                Intereses y pasatiempos:
              </Text>
            </View>
            <View style={{ width: '85%' }}>
              <Text style={[style.tableText]}>{study.studySocial.hobbies}</Text>
            </View>
          </View>
          <View style={{ width: '96%', flexDirection: 'row', marginTop: 20 }}>
            <View style={{ width: '15%' }}>
              <Text style={[style.tableText, { color: '#07DDA5', paddingRight: 5 }]}>
                Datos de salud:
              </Text>
            </View>
            <View style={{ width: '85%' }}>
              <Text style={[style.tableText]}>{study.studySocial.healthInformation}</Text>
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

export default SocialPage