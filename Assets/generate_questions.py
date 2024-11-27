import meta_ai_api
import json
import os

def generate_questions():
    ai = meta_ai_api.MetaAI()
    questions = []
    
    difficulties = ["Easy", "Medium", "Hard"]
    
    for difficulty in difficulties:
        question_response = ai.prompt(f"Generate 3 {difficulty} Java placement MCQ quiz questions in less than 30 words with four options and the correct answer, do not include code output type questions.")
        
        response_text = question_response['message']
        print(response_text)
        
        # Split the response into individual questions
        question_blocks = response_text.split("Question ")[1:]  # Ignore the part before the first question
        
        for block in question_blocks:
            try:
                # Extract the question text
                question_text_start = block.find(":", block.find("Question")) + 1
                question_text_end = block.find("\n")
                question_text = block[question_text_start:question_text_end].strip()
                
                # Extract options
                options_start = block.find("Options:") + len("Options:\n")
                options_end = block.find("\nCorrect Answer:")
                options_text = block[options_start:options_end].strip()
                options_list = options_text.split("\n")
                options = []
                for opt in options_list:
                    if ") " in opt:
                        options.append(opt.split(") ")[1].strip())
                
                # Extract correct answer
                correct_answer_start = block.find("Correct Answer:") + len("Correct Answer: ")
                correct_answer_text = block[correct_answer_start:].strip()
                correct_answer_parts = correct_answer_text.split(") ")
                correct_answer = correct_answer_parts[1] if len(correct_answer_parts) > 1 else correct_answer_text
                correct_answer = int(options.index(correct_answer))+1

                questions.append({
                    "Question": question_text,
                    "Answers": options,
                    "CorrectAnswer": correct_answer,
                    "Difficulty": difficulty
                })
            except Exception as e:
                print(f"Error processing question block: {block}")
                print(e)
    
    script_dir = os.path.dirname(__file__)
    
    # Construct the path for the JSON file
    json_file_path = os.path.join(script_dir, 'questions.json')
    
    # Save questions to a JSON file
    with open(json_file_path, "w") as file:
        json.dump(questions, file, indent=4)
    
    # Print the JSON file content for debugging
    with open(json_file_path, "r") as file:
        data = json.load(file)
        print(json.dumps(data, indent=4))

generate_questions()
