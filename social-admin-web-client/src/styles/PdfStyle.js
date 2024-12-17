import { StyleSheet, Font } from "@react-pdf/renderer";
import RalewayRegular from '../assets/fonts/Raleway/static/Raleway-Regular.ttf';

Font.register({family: "Raleway", fonts: [
  {
    src: RalewayRegular
  }
]});

export const style = StyleSheet.create({
  flexRow: {
    flexDirection: 'row',
    display: 'flex',
    alignItems: "center",
    justifyItems: "center",
  },
  center: {
    alignItems: "center",
    justifyItems: "center",
  },
  startCenter: {
    alignItems: "start",
    justifyContent: "center"
  },
  endCenter: {
    alignItems: "flex-end",
    justifyContent: "center"
  },
  generalData: {
    flex: 1,
    display: "flex",
    width: "100%",
  },
  label: {
    color: "#889DA3",
    fontSize: 16,
    fontFamily: 'Raleway',
  },
  tableText: {
    color: "#889DA3",
    fontSize: 10,
    fontFamily: 'Raleway',
    paddingLeft: 3
  },
  tableLabel: {
    color: "#0f92db",
    fontSize: 10,
    fontFamily: 'Raleway',
    paddingLeft: 3
  },
  candidateImage: {
    width: 100,
    height: 100,
    borderRadius: '50%',
    margin: 20
  },
  tittle: {
    textAlign: 'center',
    color: "#0f92db",//"#1bb9ef",
    fontSize: 26,
    fontFamily: 'Raleway',
    fontWeight: 700,
    gridColumnStart: 1,
    gridColumnEnd: 3,
    textTransform: "uppercase"
  },
  generalDataSection: {
    alignItems: "center",
    display: "flex",
    justifyItems: "center",
    width: '100%',
  },
  generalDataItem: {
    display: "flex",
    flexDirection: "row",
    width: 565,
  },
  boxSmall: {
    display: 'flex',
    width: '24%',
    border: 0.5,
    borderColor: 'gray',
    paddingVertical: 5
  },
  boxBig: {
    display: 'flex',
    width: '48%',
    border: 0.5,
    borderColor: 'gray',
  }
});