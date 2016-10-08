Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml
Imports System.Xml.Linq


module programa
	sub main()
	
	
	
	console.writeline("                      FEITO POR CARLOS MORETTI - TURMA 1242")
	console.writeline()
	console.writeline()
	console.writeline()
	console.writeline()
	console.writeline()
	
	
	
	Dim cep as String
	
	console.writeline("Digite o CEP abaixo para realizar a pesquisa:")
	cep = console.readline
	
		'DOWNLOAD DE DADOS
	Dim request as HttpWebRequest
	Dim response as HttpWebResponse
	Dim Webstream as Stream
	Dim WebResponse = ""
	request = CType(WebRequest.Create("http://apps.widenet.com.br/busca-cep/api/cep/" & cep & ".xml"), HttpWebRequest)
	response = CType(request.GetResponse(), HttpWebResponse) ' Send Request
	webStream = response.GetResponseStream() ' Get Response
	Dim webStreamReader As New StreamReader(webStream)
	While webStreamReader.Peek >= 0
		webResponse = webStreamReader.ReadToEnd()
	End While
	
	'FIM DOWNLOAD DE DADOS
	
	My.Computer.FileSystem.WriteAllText("save.xml", WebResponse,True)
	
	'' - INFORMA AO USUÁRIO INFORMAÇÕES DA MORADA
	try
		dim documentoxml as XmlDocument
		dim nodelist as XmlNodeList
		dim nodo as XmlNode
		documentoxml = New XmlDocument
		documentoxml.Load("save.xml")
		nodelist = documentoxml.SelectNodes("/cep")
		for each nodo in nodelist
			dim nodo1 = nodo.ChildNodes(1).InnerText
			dim nodo2 = nodo.ChildNodes(2).InnerText
			dim nodo3 = nodo.ChildNodes(3).InnerText
			dim nodo4 = nodo.ChildNodes(4).InnerText
			dim nodo5 = nodo.ChildNodes(5).InnerText
			console.clear
			console.writeline("CEP: " & nodo1)
			console.writeline("UF/Estado: " & nodo2)
			console.writeline("Cidade: " & nodo3)
			console.writeline("Bairro: " & nodo4)
			console.writeline("Endereço: " & nodo5)
		next
	catch
		console.writeline("CEP não encontrado.")
	end try
	System.IO.File.WriteAllText("save.xml","")

	console.read
		
	end sub
end module
	