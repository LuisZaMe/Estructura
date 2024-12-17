import React from 'react';
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfFooter from './PdfFooter';
import PdfHeader from './PdfHeader';

const FamilyPage = ({ study }) => {
  const maritalStatus = {
    1: "Soltero(a)",
    2: "Casado(a)",
    3: "Vieworciado(a)",
    4: "Separación en proceso judicial",
    5: "Viudo(a)",
    6: "Concubinato"
  };

  return (
    <Page size={"A4"} style={{ width: '100%' }}>
      <View fixed>
        <PdfHeader />
      </View>
      <View style={style.generalData}>
        <View style={style.generalDataSection}>
          <Text style={[style.label, { color: '#07DDA5', marginVertical: 10, }]}>B. FAMILIA</Text>
          <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>FAMILIA CONVIVIENTE</Text>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nombre</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Parentesco</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Edad</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Estado civil</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Escolaridad</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Domicilio</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Tel.</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Ocupación</Text>
            </View>
          </View>
          {
            study.studyFamily.livingFamilyList.map(member => {
              return (
                <View style={{ flexDirection: 'row' }}>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.name}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.relationship}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.age}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{maritalStatus[member.maritalStatus]}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.schoolarity}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.address}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.phone}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.occupation}</Text>
                  </View>
                </View>
              )
            })
          }
          <View style={{ width: '96%', flexDirection: 'row', marginTop: 20 }}>
            <View style={{ width: '10%' }}>
              <Text style={[style.tableText, { color: '#07DDA5', paddingRight: 5 }]}>
                Nota:
              </Text>
            </View>
            <View style={{ width: '90%' }}>
              <Text style={[style.tableText]}>
                {study.studyFamily.notes}
              </Text>
            </View>
          </View>

          <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>FAMILIA NO CONVIVIENTE</Text>
          <View style={{ flexDirection: 'row' }}>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nombre</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Parentesco</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Edad</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Estado civil</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Escolaridad</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Domicilio</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Tel.</Text>
            </View>
            <View style={[style.boxBig, style.startCenter, { width: '12%' }]}>
              <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Ocupación</Text>
            </View>
          </View>
          {
            study.studyFamily.noLivingFamilyList.map(member => {
              return (
                <View style={{ flexDirection: 'row' }}>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.name}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.relationship}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.age}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{maritalStatus[member.maritalStatus]}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.schoolarity}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.address}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.phone}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '12%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{member.occupation}</Text>
                  </View>
                </View>
              )
            })
          }

          <View style={{ width: '96%', flexDirection: 'row', marginTop: 20 }}>
            <View style={{ width: '15%' }}>
              <Text style={[style.tableText, { color: '#07DDA5', paddingRight: 5 }]}>
                Área familiar:
              </Text>
            </View>
            <View style={{ width: '85%' }}>
              <Text style={[style.tableText]}>
                {study.studyFamily.familiarArea}
              </Text>
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

export default FamilyPage