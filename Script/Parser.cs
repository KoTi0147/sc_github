using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialgoueParser : MonoBehaviour
{

    public Dialgoue[] Parse(string _CSVFilieName)
    {
        List<Dialgoue> dialgoueList = new List<Dialgoue>(); // ��� ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFilieName); // CSV �����͸� �ޱ� ���� �׸� 

        string[] data = csvData.text.Split(new char[] { '\n' }); //���͸� ������ �ɰ��� ����
        //���͸� ������ data[0] - ������Ʈ�� �� 1��° �� �ǹ� 

        for (int i = 1; i < data.Length;) // i++�� ���� ������ �״��� ������ ���ǹ��� ���ؼ� 
        {
            string[] row = data[i].Split(new char[] { ',' }); //, ������ row �ٿ� ����

            Dialgoue dialgoue = new Dialgoue(); // ��� ����Ʈ ����
            dialgoue.name = row[1];
            List<string> contextList = new List<string>(); // ��� ����Ʈ ����
            List<string> EventList = new List<string>(); // �̺�Ʈ �ѹ� ����
            List<string> SkipList = new List<string>(); // ���� �ǳ��� ��� �߰� ���ϸ� ������

            //dialgoue.contexts = row[2]; // �迭�� ũ�⸦ �̸� ��������ߵǴµ� ������ �ְ��־ �� ����Ʈ�� �̿�
            do
            {
                contextList.Add(row[2]);
                EventList.Add(row[3]);
                SkipList.Add(row[4]);

                if (++i < data.Length) // i�� �̸� ������ ���¿��� �����ش� dataLentg���� �۴ٸ�
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }

            } while (row[0].ToString() == ""); // ���� 1ȸ ���� �� ���� �� ���� �����Ű�� ���ǹ��� ��
                                               // row 0��° �ٿ��� ID�� �� �ְ� Tostring���� �� �������� ������


            dialgoue.contexts = contextList.ToArray();
            dialgoue.number = EventList.ToArray();
            dialgoue.skipnum = SkipList.ToArray();

            dialgoueList.Add(dialgoue);

            GameObject obj = GameObject.Find("DialgoueManager");
            obj.GetComponent<interactionEvent>().lineY = dialgoueList.Count;

        }

        return dialgoueList.ToArray();
    }


}
